using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Events.Integration.Protobuf.Workflow;
using Google.Type;
using Marten;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Event.OperationalFunding;
using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Domain.Model.OperationalFunding;
using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.ECE.Events.Integration;
using Moe.Library.Cqrs;
using ProtoBuf.Bcl;
using IntegrationEvents = Events.Integration.Protobuf;
namespace MoE.ECE.Domain.Integration.Publisher
{
    public class IntegrationEventPublisher :
        IDomainEventHandler<Rs7Updated>,
        IDomainEventHandler<Rs7Approved>,
        IDomainEventHandler<FullRs7Created>,
        IDomainEventHandler<Rs7ZeroReturnCreated>,
        IDomainEventHandler<Rs7EntitlementMonthUpdated>,
        IDomainEventHandler<Rs7SubmittedForApproval>,
        IDomainEventHandler<Rs7PeerApproved>,
        IDomainEventHandler<Rs7PeerRejected>,
        IDomainEventHandler<OperationalFundingRequestCreated>
    {
        private readonly IServiceBus _serviceBus;
        private readonly IMapper _mapper;
        private readonly IDocumentSession _documentSession;
        private readonly ReferenceDataContext _referenceDataContext;

        public IntegrationEventPublisher(IServiceBus serviceBus, IMapper mapper, IDocumentSession documentSession, ReferenceDataContext referenceDataContext)
        {
            _serviceBus = serviceBus;
            _mapper = mapper;
            _documentSession = documentSession;
            _referenceDataContext = referenceDataContext;
        }

        public Task Handle(Rs7EntitlementMonthUpdated domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = _mapper.Map<IntegrationEvents.Roll.Rs7Updated>(domainEvent);

            return _serviceBus.PublishAsync(integrationEvent, Constants.Topic.ECE, cancellationToken);
        }

        public Task Handle(Rs7Updated domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = _mapper.Map<IntegrationEvents.Roll.Rs7Updated>(domainEvent);

            return _serviceBus.PublishAsync(integrationEvent, Constants.Topic.ECE, cancellationToken);
        }

        public Task Handle(Rs7Approved domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = _mapper.Map<IntegrationEvents.Roll.Rs7Updated>(domainEvent);

            var rs7UpdatedTask = _serviceBus.PublishAsync(integrationEvent, Constants.Topic.ECE, cancellationToken);

            var fundingEntitlementUpdatedTask = PublishFundingEntitlementUpdated(cancellationToken, domainEvent);

            return Task.WhenAll(rs7UpdatedTask, fundingEntitlementUpdatedTask);
        }

        public Task Handle(Rs7PeerRejected domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = _mapper.Map<IntegrationEvents.Roll.Rs7Updated>(domainEvent);

            return _serviceBus.PublishAsync(integrationEvent, Constants.Topic.ECE, cancellationToken);
        }

        public Task Handle(FullRs7Created domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = _mapper.Map<IntegrationEvents.Roll.Rs7Updated>(domainEvent);

            return _serviceBus.PublishAsync(integrationEvent, Constants.Topic.ECE, cancellationToken);
        }

        public Task Handle(Rs7ZeroReturnCreated domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = _mapper.Map<IntegrationEvents.Roll.Rs7Updated>(domainEvent);

            return _serviceBus.PublishAsync(integrationEvent, Constants.Topic.ECE, cancellationToken);
        }

        public Task Handle(Rs7SubmittedForApproval domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = _mapper.Map<IntegrationEvents.Roll.Rs7Updated>(domainEvent);

            return _serviceBus.PublishAsync(integrationEvent, Constants.Topic.ECE, cancellationToken);
        }

        public Task Handle(Rs7PeerApproved domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = _mapper.Map<IntegrationEvents.Roll.Rs7Updated>(domainEvent);

            return _serviceBus.PublishAsync(integrationEvent, Constants.Topic.ECE, cancellationToken);
        }
        
        public Task Handle(OperationalFundingRequestCreated domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = _mapper.Map<IntegrationEvents.Entitlement.EntitlementCalculated>(domainEvent);

            return _serviceBus.PublishAsync(
                integrationEvent,
                Constants.Topic.ECE,
                cancellationToken);
        }
        
        private async Task PublishFundingEntitlementUpdated(CancellationToken cancellationToken,
            Rs7Model domainEvent)
        {
            var operationalFundingRequest = await _documentSession
                    .Query<OperationalFundingRequest>()
                    .SingleOrDefaultAsync(model => model.BusinessEntityId == domainEvent.BusinessEntityId 
                                              && model.RevisionNumber == domainEvent.RevisionNumber, cancellationToken);
            
            var service = await _referenceDataContext.EceServices.FindAsync(domainEvent.OrganisationId);
            
            var organisationNumber = service?.OrganisationNumber ?? string.Empty;
            
            var fundingEntitlementUpdated = new FundingEntitlementUpdated
            {
                Id = new Guid(domainEvent.BusinessEntityId.GetValueOrDefault()),
                Description =
                    $"{Constants.BusinessEntityTypes.Rs7} request: {domainEvent.RequestId}",
                EntitlementType = Constants.BusinessEntityTypes.Rs7,
                // NEED TO GET FROM OPA NEED TO GET FROM OPA
                PaymentNzDate = new Date
                {
                    Year = System.DateTime.UtcNow.AddDays(1).Year, Month = System.DateTime.UtcNow.AddDays(1).Month, Day = System.DateTime.UtcNow.AddDays(1).Day
                },
                PayeeOrganisationNumber = organisationNumber,
                ParentPayeeOrganisationNumber = organisationNumber,
                SourceSystem = Constants.Topic.ECE,
                BusinessEntityId = new Guid(domainEvent.BusinessEntityId.GetValueOrDefault()),
                LineItems =
                {
                    new InvoiceLineItem
                    {
                        AmountGstExclusive = new Decimal(operationalFundingRequest.TotalWashUp.GetValueOrDefault()),
                        AmountGstInclusive = new Decimal(operationalFundingRequest.TotalWashUp.GetValueOrDefault()),
                        Description = $"Line item 1"
                    }
                },

                Status = FundingEntitlementUpdatedStatus.EntitlementApproved
            };

            await _serviceBus.PublishAsync(
                fundingEntitlementUpdated,
                Constants.Topic.ECE,
                cancellationToken);
        }
    }
}