using Events.Integration;
using MediatR;

namespace MoE.ECE.Domain.Integration
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : INotificationHandler<TIntegrationEvent>
        where TIntegrationEvent : IIntegrationEvent
    {
    }
}