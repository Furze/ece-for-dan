using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Event.OperationalFunding;
using Moe.ECE.Events.Integration;
using Moe.Library.Cqrs;
using IntegrationEvents = Events.Integration.Protobuf;
namespace MoE.ECE.Domain.Integration.Publisher
{
    public class IntegrationEventPublisher :
        IDomainEventHandler<Rs7Updated>,
        IDomainEventHandler<Rs7Approved>,
        IDomainEventHandler<Rs7CreatedFromExternal>,
        IDomainEventHandler<Rs7ZeroReturnCreated>,
        IDomainEventHandler<Rs7EntitlementMonthUpdated>,
        IDomainEventHandler<Rs7SubmittedForApproval>,
        IDomainEventHandler<Rs7PeerApproved>,
        IDomainEventHandler<Rs7PeerRejected>,
        IDomainEventHandler<OperationalFundingRequestCreated>
    {
        private readonly IServiceBus _serviceBus;
        private readonly IMapper _mapper;

        public IntegrationEventPublisher(IServiceBus serviceBus, IMapper mapper)
        {
            _serviceBus = serviceBus;
            _mapper = mapper;
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

            return _serviceBus.PublishAsync(integrationEvent, Constants.Topic.ECE, cancellationToken);
        }

        public Task Handle(Rs7PeerRejected domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = _mapper.Map<IntegrationEvents.Roll.Rs7Updated>(domainEvent);

            return _serviceBus.PublishAsync(integrationEvent, Constants.Topic.ECE, cancellationToken);
        }

        public Task Handle(Rs7CreatedFromExternal domainEvent, CancellationToken cancellationToken)
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
                integrationEvent ,
                Constants.Topic.ECE,
                cancellationToken);
        }
    }
}