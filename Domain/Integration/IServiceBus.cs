using System.Threading;
using System.Threading.Tasks;
using Moe.ECE.Events.Integration;

namespace MoE.ECE.Domain.Integration
{
    public interface IServiceBus
    {
        Task PublishAsync<TEvent>(
            TEvent integrationEvent,
            string topic,
            CancellationToken cancellationToken)
            where TEvent : IIntegrationEvent;
    }
}