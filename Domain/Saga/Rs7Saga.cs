using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marten;
using MediatR;
using Microsoft.Extensions.Internal;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.ReferenceData;
using MoE.ECE.Domain.Model.Rs7;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.Library.Cqrs;
using static MoE.ECE.Domain.Exceptions.DomainExceptions;

namespace MoE.ECE.Domain.Saga
{
     public class Rs7Saga :
        IBeginASaga<CreateSkeletonRs7>,
        IBeginASaga<CreateRs7ZeroReturn>,
        IHandleACommand<SaveAsDraft>,
        IHandleACommand<UpdateRs7>,
       
        IHandleACommand<DiscardRs7>,
        IHandleACommand<CreateFullRs7>,
        
        IHandleACommand<UpdateRs7EntitlementMonth>,
        IHandleACommand<UpdateRs7Declaration>
    {
        private readonly ICqrs _cqrs;
        private readonly IMapper _mapper;
        private readonly ReferenceDataContext _refDataContext;
        private readonly ISystemClock _systemClock;
        private readonly IDocumentSession _documentSession;

        public Rs7Saga(
            IDocumentSession documentSession,
            ReferenceDataContext refDataContext,
            ICqrs cqrs,
            IMapper mapper,
            ISystemClock systemClock)
        {
            _documentSession = documentSession;
            _refDataContext = refDataContext;
            _cqrs = cqrs;
            _mapper = mapper;
            _systemClock = systemClock;
        }

        public async Task<int> Handle(CreateSkeletonRs7 command, CancellationToken cancellationToken)
        {
            // check the service is eligible for a new rs7
            ServiceAndEligibilityCheck(command.OrganisationId.GetValueOrDefault(), true);
            
            var rs7 = await LoadOrCreateRs7(command.OrganisationId.GetValueOrDefault(), command.FundingPeriod, command.FundingPeriodYear, cancellationToken);

            rs7.CreateSkeleton(_systemClock.UtcNow, RollStatus.ExternalNew);

            await SaveAndRaiseAsync<Rs7SkeletonCreated>(rs7, cancellationToken);

            return rs7.Id;
        }

        public async Task<int> Handle(CreateRs7ZeroReturn command, CancellationToken cancellationToken)
        {
            // check the service is eligible for a new rs7. License checks are not performed for Zero-returns.
            ServiceAndEligibilityCheck(command.OrganisationId.GetValueOrDefault(), false);
            
            var rs7 = await LoadOrCreateRs7(command.OrganisationId.GetValueOrDefault(), command.FundingPeriod, command.FundingPeriodYear, cancellationToken);

            rs7.CreateZeroReturn(_systemClock.UtcNow);

            await SaveAndRaiseAsync<Rs7ZeroReturnCreated>(rs7, cancellationToken);
            
            return rs7.Id;
        }

        public async Task<Unit> Handle(CreateFullRs7 command, CancellationToken cancellationToken)
        {
            ServiceAndEligibilityCheck(command.OrganisationId.GetValueOrDefault(), true);
            
            var rs7 = await LoadOrCreateRs7(command.OrganisationId.GetValueOrDefault(), command.FundingPeriod, command.FundingPeriodYear, cancellationToken);

            // Go straight to InternalReadyForReview
            rs7.CreateSkeleton(_systemClock.UtcNow, RollStatus.InternalReadyForReview);
           
            // Now map in the details..
            _mapper.Map(command, rs7);

            await SaveAndRaiseAsync<Rs7CreatedFromExternal>(rs7, cancellationToken);
            
            return Unit.Value;
        }

        public async Task<Unit> Handle(DiscardRs7 command, CancellationToken cancellationToken)
        {
            var rs7 = await _documentSession.LoadRs7Async(command.Id, cancellationToken);

            if (rs7.CanBeDiscarded() == false)
                throw InvalidRollStatusForDiscard(rs7.RollStatus);

            _documentSession.Delete<Rs7>(command.Id);

            await _documentSession.SaveChangesAsync(cancellationToken);

            var domainEvent = new Rs7Discarded(rs7, _systemClock.UtcNow);

            await _cqrs.RaiseEventAsync(domainEvent, cancellationToken);

            return Unit.Value;
        }

        public Task<Unit> Handle(SaveAsDraft command, CancellationToken cancellationToken)
        {
            return DoUpdateRs7(command, cancellationToken);
        }

        public Task<Unit> Handle(UpdateRs7 command, CancellationToken cancellationToken)
        {
            return DoUpdateRs7(command, cancellationToken);
        }

        public async Task<Unit> Handle(UpdateRs7EntitlementMonth command, CancellationToken cancellationToken)
        {
            var rs7 = await _documentSession.LoadRs7Async(command.Id, cancellationToken);

            ServiceAndEligibilityCheck(rs7.OrganisationId, true);
     
            //TODO: fix mapping issues and map into rs7 rather than revision..
            // apply the updates
            _mapper.Map(command, rs7.CurrentRevision);

            rs7.WasUpdated(_systemClock.UtcNow);

            _documentSession.Update(rs7);
            
            await SaveAndRaiseAsync<Rs7EntitlementMonthUpdated>(rs7, cancellationToken);
           
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateRs7Declaration command, CancellationToken cancellationToken)
        {
            var rs7 = await _documentSession.LoadRs7Async(command.Id, cancellationToken);

            rs7.UpdateDeclaration(command);

            await SaveAndRaiseAsync<Rs7DeclarationUpdated>(rs7, cancellationToken);
           
            return Unit.Value;
        }

        private async Task<Unit> DoUpdateRs7<TSource>(TSource command, CancellationToken cancellationToken) where TSource : Rs7Model
        {
            var rs7 = await _documentSession.LoadRs7Async(command.Id, cancellationToken);

            ServiceAndEligibilityCheck(rs7.OrganisationId, true);

            rs7.UpdateRollStatus(command.RollStatus, _systemClock.UtcNow);

            // apply the updates
            _mapper.Map(command, rs7.CurrentRevision);

            _documentSession.Update(rs7);
            
            await SaveAndRaiseAsync<Rs7Updated>(rs7, cancellationToken);

            return Unit.Value;
        }

       

        /// <summary>
        /// Checks the Service actually exists and when required, checks their license status is eligible for an Rs7.
        /// </summary>
        /// <param name="organisationId"></param>
        /// <param name="checkLicense"></param>
        /// <exception cref="BadRequestException">Thrown when the service doesn't exist, or when license checks performed and the license is not eligible</exception>
        private void ServiceAndEligibilityCheck(int organisationId, bool checkLicense)
        {
            var eceService = _refDataContext.EceServices.FindNullable(organisationId);
            if (eceService == null)
                throw EceServiceNotFound(organisationId);

            if (checkLicense
            && (eceService.LicenceStatusId == LicenceStatus.Cancelled || eceService.LicenceStatusId == LicenceStatus.Suspended))
            {
                throw EceServiceIneligibleBecauseLicenceStatus(eceService.OrganisationName, eceService.RefOrganisationId,
                    eceService.LicenceStatusDescription, eceService.LicenceStatusId);
            }

            //TODO(ERST-11367): Originally part of ERST-11035, but couldn't be implemented until Closed status records actually imported from First.
            //else if (eceService.OrganisationStatusId == OrganisationStatus.Closed && was-closed-over-12-months-ago)
            //{
            //    throw EceServiceIneligibleBecauseStatusClosed(eceService.ServiceName, eceService.RefOrganisationId);
            //}
        }

        /// <summary>
        /// Checks for duplicate Rs7. If a New status one exists it is returned.
        /// </summary>
        /// <param name="organisationId"></param>
        /// <param name="fundingPeriod"></param>
        /// <param name="fundingPeriodYear"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns Null, or an existing New status Rs7 if found</returns>
        /// <exception cref="BadRequestException">Thrown when a duplicate Rs7 is found, and is not New status</exception>
        private async Task<Rs7> LoadOrCreateRs7(int organisationId, FundingPeriodMonth fundingPeriod, int fundingPeriodYear, CancellationToken cancellationToken)
        {
            // check for duplicates
            var rs7 = await _documentSession.Query<Rs7>()
                .Where(r => r.OrganisationId == organisationId
                            && r.FundingPeriod == fundingPeriod
                            && r.FundingPeriodYear == fundingPeriodYear)
                .SingleOrDefaultAsync(cancellationToken);

            if (rs7 == null)
            {
                // make a new Rs7
                rs7 = new Rs7(organisationId, fundingPeriod, fundingPeriodYear);
                
                _documentSession.Insert(rs7);
            }
            else
            {
                // if one already exists with New status, it's likely dormant and can be reused.
                if (rs7.RollStatus == RollStatus.ExternalNew)
                {
                    _documentSession.Update(rs7);
                }
                else
                {
                    // otherwise, throw duplicate error. A new one cannot be created
                    throw DuplicateResourceFundingPeriod<Rs7>(organisationId, fundingPeriod.ToString(), fundingPeriodYear);    
                }
            }

            return rs7;
        }
        
        private async Task SaveAndRaiseAsync<TDomainEvent>(Rs7 rs7, CancellationToken cancellationToken) where TDomainEvent : IDomainEvent
        {
            await _documentSession.SaveChangesAsync(cancellationToken);

            var domainEvent = _mapper.Map<TDomainEvent>(rs7);
            
            await _cqrs.RaiseEventAsync(domainEvent, cancellationToken);
        }
    }
}