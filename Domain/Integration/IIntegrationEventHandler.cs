using MediatR;
using Moe.ECE.Events.Integration;

namespace MoE.ECE.Domain.Integration
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : INotificationHandler<TIntegrationEvent>
        where TIntegrationEvent : IIntegrationEvent
    {
    }
}