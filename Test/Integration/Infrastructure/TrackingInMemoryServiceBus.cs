using System.Threading;
using System.Threading.Tasks;
using MoE.ECE.Domain.Integration;
using Moe.ECE.Events.Integration;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    public class TrackingInMemoryServiceBus : IServiceBus
    {
        private readonly IEventTracker<IIntegrationEvent> _integrationEventTracker;
        private readonly IServiceBus _serviceBusImplementation;

        public TrackingInMemoryServiceBus(InMemoryServiceBus serviceBusImplementation,
            IEventTracker<IIntegrationEvent> integrationEventTracker)
        {
            _serviceBusImplementation = serviceBusImplementation;
            _integrationEventTracker = integrationEventTracker;
        }

        public Task PublishAsync<TEvent>(TEvent integrationEvent, string source, CancellationToken cancellationToken)
            where TEvent : IIntegrationEvent
        {
            _integrationEventTracker.Add(integrationEvent);
            return _serviceBusImplementation.PublishAsync(integrationEvent, source, cancellationToken);
        }
    }
}