using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Events.Integration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Integration;
using MoE.ECE.Web.Infrastructure.Settings;
using Moe.Library.ServiceBus;

namespace MoE.ECE.Web.Infrastructure.ServiceBus
{
    public class ServiceBusPublisher : IServiceBus
    {
        private readonly ILogger<ServiceBusPublisher> _logger;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly MessageFactory _messageFactory;

        public ServiceBusPublisher(IOptions<ConnectionStrings> options, ILogger<ServiceBusPublisher> logger, MessageFactory messageFactory)
        {
            _logger = logger;
            _serviceBusClient = new ServiceBusClient(options.Value.ServiceBus);
            _messageFactory = messageFactory;
        }

        public async Task PublishAsync<TEvent>(TEvent integrationEvent, string topic,
            CancellationToken cancellationToken) where TEvent : IIntegrationEvent
        {
            var outgoingMessage = _messageFactory.CreateOutgoingMessage(integrationEvent, topic);

            await SendAsync(outgoingMessage);

            Log(outgoingMessage);
        }

        protected virtual Task SendAsync(OutgoingMessage outgoingMessage)
        {
            var serviceBusSender = _serviceBusClient.CreateSender(outgoingMessage.Topic);
            if (serviceBusSender != null)
            {
                return serviceBusSender.SendMessageAsync(outgoingMessage.ServiceBusMessage);
            }

            throw new ECEApplicationException("Could not get topic client");
        }

        private void Log(IMessageWrapper message)
        {
            const string formattedMessage = "[Service Bus Message Published] :{objectType} {objectJson} {cqrsType}";

            _logger.LogDebug(formattedMessage, message.PayloadType.Name,
                JsonSerializer.Serialize(message.Payload, new JsonSerializerOptions {WriteIndented = true}), "Integration Event");
        }
    }
}