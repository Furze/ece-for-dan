﻿using System.Linq;
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
        IBeginASaga<CreateRs7>,
        IBeginASaga<CreateRs7ZeroReturn>,
        IHandleACommand<SaveAsDraft>,
        IHandleACommand<UpdateRs7>,
       
        IHandleACommand<DiscardRs7>,
        IHandleACommand<CreateRs7FromExternal>,
        
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

        public async Task<int> Handle(CreateRs7 command, CancellationToken cancellationToken)
        {
            // check the service is eligible for a new rs7
            ServiceAndEligibilityCheck(command.OrganisationId.GetValueOrDefault(), true);
            
            var rs7 = await LoadRs7(command.OrganisationId.GetValueOrDefault(), command.FundingPeriod, command.FundingPeriodYear, cancellationToken);

            rs7.CreatedByExternalUser(_systemClock.UtcNow);
            
            await _documentSession.SaveChangesAsync(cancellationToken);

            var domainEvent = _mapper.Map<Rs7Created>(rs7);
            
            await _cqrs.RaiseEventAsync(domainEvent, cancellationToken);

            return rs7.Id;
        }

        public async Task<int> Handle(CreateRs7ZeroReturn command, CancellationToken cancellationToken)
        {
            // check the service is eligible for a new rs7. License checks are not performed for Zero-returns.
            ServiceAndEligibilityCheck(command.OrganisationId.GetValueOrDefault(), false);
            
            var rs7 = await LoadRs7(command.OrganisationId.GetValueOrDefault(), command.FundingPeriod, command.FundingPeriodYear, cancellationToken);

            rs7.SetAsZeroReturn(_systemClock.UtcNow);

            await _documentSession.SaveChangesAsync(cancellationToken);

            var domainEvent = _mapper.Map<Rs7ZeroReturnCreated>(rs7);
            
            await _cqrs.RaiseEventAsync(domainEvent, cancellationToken);

            return rs7.Id;
        }

        public async Task<Unit> Handle(CreateRs7FromExternal command, CancellationToken cancellationToken)
        {
            ServiceAndEligibilityCheck(command.OrganisationId.GetValueOrDefault(), true);
            
            var rs7 = await LoadRs7(command.OrganisationId.GetValueOrDefault(), command.FundingPeriod, command.FundingPeriodYear, cancellationToken);

            rs7.CreatedFromExternalSystem(_systemClock.UtcNow);
           
            _mapper.Map(command, rs7);

            await _documentSession.SaveChangesAsync(cancellationToken);

            var domainEvent = _mapper.Map<Rs7CreatedFromExternal>(rs7);
            
            await _cqrs.RaiseEventAsync(domainEvent, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DiscardRs7 command, CancellationToken cancellationToken)
        {
            var rs7 = await _documentSession.LoadAsync<Rs7>(command.Id, cancellationToken);

            if (rs7 == null)
                throw ResourceNotFoundException<Rs7>(command.Id);

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
            return DoUpdateRs7(command, Source.Internal, cancellationToken);
        }

        public Task<Unit> Handle(UpdateRs7 command, CancellationToken cancellationToken)
        {
            return DoUpdateRs7(command, Source.Internal, cancellationToken);
        }

        public async Task<Unit> Handle(UpdateRs7EntitlementMonth command, CancellationToken cancellationToken)
        {
            var rs7 = await _documentSession.LoadAsync<Rs7>(command.Id, cancellationToken);

            if (rs7 == null)
                throw ResourceNotFoundException<Rs7>(command.Id);

            ServiceAndEligibilityCheck(rs7.OrganisationId, true);
     
            //TODO: fix mapping issues and map into rs7 rather than revision..
            // apply the updates
            _mapper.Map(command, rs7.CurrentRevision);

            rs7.EntitlementMonthUpdated(_systemClock.UtcNow);

            _documentSession.Update(rs7);
            
            await _documentSession.SaveChangesAsync(cancellationToken);

            var domainEvent = _mapper.Map<Rs7EntitlementMonthUpdated>(rs7);
            
            await _cqrs.RaiseEventAsync(domainEvent, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateRs7Declaration command, CancellationToken cancellationToken)
        {
            var rs7 = await _documentSession.LoadAsync<Rs7>(command.Id, cancellationToken);

            if (rs7 == null)
                throw ResourceNotFoundException<Rs7>(command.Id);

            rs7.UpdateDeclaration(command);
            
            //_mapper.Map(command, rs7.CurrentRevision.Declaration);

            await _documentSession.SaveChangesAsync(cancellationToken);

            var domainEvent = _mapper.Map<Rs7DeclarationUpdated>(rs7);

            await _cqrs.RaiseEventAsync(domainEvent, cancellationToken);

            return Unit.Value;
        }

        private async Task<Unit> DoUpdateRs7<TSource>(TSource command, string source, CancellationToken cancellationToken) where TSource : Rs7Model
        {
            var rs7 = await _documentSession.LoadAsync<Rs7>(command.Id, cancellationToken);

            if (rs7 == null)
                throw ResourceNotFoundException<Rs7>(command.Id);

            // updates to Zero Returns convert to a normal Rs7 - ensure the service is eligible.
            if (rs7.IsZeroReturn)
            {
                ServiceAndEligibilityCheck(rs7.OrganisationId, true);
            }

            // Don't allow save with New status. The consumer should provide Draft or PendingApproval
            if (command.RollStatus == RollStatus.ExternalNew)
                throw InvalidUpdateRs7StatusNew();

            // Don't let the status move back into Draft!
            if (command.RollStatus == RollStatus.ExternalDraft && rs7.RollStatus > RollStatus.ExternalDraft)
                throw InvalidRollStatusTransition(rs7.RollStatus, command.RollStatus);

            // Once submitted, can not update roll data
            if (rs7.RollStatus != RollStatus.ExternalNew 
                && rs7.RollStatus != RollStatus.ExternalDraft 
                && rs7.RollStatus != RollStatus.ExternalSubmittedForApproval  
                && rs7.RollStatus != RollStatus.ExternalReturnedForEdit)
                throw InvalidRollStatusForUpdate(rs7.RollStatus);
            
            // if the current Rs7 had already passed Draft status we create a new revision for the updates
            if (rs7.RollStatus > RollStatus.ExternalDraft)
            {
                rs7.CloneNextRevision(_systemClock.UtcNow);
                //only change the Source on a new revision, because we want the original Revision's Source to
                //retain whatever value it was created with.
                rs7.CurrentRevision.Source = source;
            }

            // apply the updates
            _mapper.Map(command, rs7.CurrentRevision);
            rs7.CurrentRevision.IsZeroReturn = false;
            rs7.RollStatus = command.RollStatus;
            rs7.CurrentRevision.UpdateRevisionDate(_systemClock);

            _documentSession.Update(rs7);
            await _documentSession.SaveChangesAsync(cancellationToken);

            var domainEvent = _mapper.Map<Rs7Updated>(rs7);

            await _cqrs.RaiseEventAsync(domainEvent, cancellationToken);

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
        private async Task<Rs7> LoadRs7(int organisationId, FundingPeriodMonth fundingPeriod, int fundingPeriodYear, CancellationToken cancellationToken)
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
    }
}