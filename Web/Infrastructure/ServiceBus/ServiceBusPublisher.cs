using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoE.ECE.Domain.Exceptions;
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
            var outgoingMessage = new OutgoingMessage(integrationEvent, topic, MessageFormat.Proto);

            await SendAsync(outgoingMessage);

            Log(outgoingMessage);
        }

        protected virtual Task SendAsync(OutgoingMessage outgoingMessage)
        {
            //TODO: Pull Service Bus Client from IOC to pool connections
            var serviceBusConnection = new ServiceBusClient(_serviceBusConnectionString);
            var serviceBusSender = serviceBusConnection.CreateSender(outgoingMessage.Topic);
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