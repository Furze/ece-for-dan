using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoE.ECE.Domain.Integration;
using Moe.ECE.Events.Integration;
using MoE.ECE.Web.Infrastructure.Settings;
using Moe.Library.ServiceBus;

namespace MoE.ECE.Web.Infrastructure.ServiceBus
{
    public class ServiceBusPublisher : IServiceBus
    {
        private readonly ILogger<ServiceBusPublisher> _logger;
        private readonly string _serviceBusConnectionString;

        public ServiceBusPublisher(IOptions<ConnectionStrings> options, ILogger<ServiceBusPublisher> logger)
        {
            _logger = logger;
            _serviceBusConnectionString = options.Value.ServiceBus;
        }

        public async Task PublishAsync<TEvent>(TEvent integrationEvent, string topic,
            CancellationToken cancellationToken) where TEvent : IIntegrationEvent
        {
            var messageFormat = typeof(IMessage).IsAssignableFrom(typeof(TEvent))
                ? MessageFormat.Proto
                : MessageFormat.Json;

            var outgoingMessage = new OutgoingMessage(integrationEvent, topic, messageFormat);

            await SendAsync(outgoingMessage);

            Log(outgoingMessage);
        }

        protected virtual Task SendAsync(OutgoingMessage outgoingMessage)
        {
            var topicClient = new TopicClient(_serviceBusConnectionString, outgoingMessage.Topic);

            return topicClient.SendAsync(outgoingMessage.ServiceBusMessage);
        }

        private void Log(IMessageWrapper message)
        {
            const string formattedMessage = "[Service Bus Message Published] :{objectType} {objectJson}";

            _logger.LogDebug(formattedMessage, message.PayloadType.Name,
                JsonSerializer.Serialize(message.Payload, new JsonSerializerOptions {WriteIndented = true}));
        }
    }
}