using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoE.ECE.Web.Infrastructure.Settings;
using Moe.Library.ServiceBus;
using Newtonsoft.Json;

namespace MoE.ECE.Web.Infrastructure.ServiceBus
{
    /// <summary>
    ///     Reads messages from our Service Bus Queue deserializes them then broadcasts them to Mediatr
    /// </summary>
    public abstract class ServiceBusConsumer : BackgroundService
    {
        private readonly string _connectionString;
        private readonly ILogger<ServiceBusConsumer> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly Lazy<ServiceBusProcessor> _subscriptionProcessor;
        private readonly MessageFactory _messageFactory;

        protected ServiceBusConsumer(
            IOptions<ConnectionStrings> options,
            ILogger<ServiceBusConsumer> logger,
            IServiceProvider serviceProvider,
            MessageFactory messageFactory)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _connectionString = options.Value.ServiceBus;
            _subscriptionProcessor = new Lazy<ServiceBusProcessor>(CreateSubscriptionClient);
            _messageFactory = messageFactory;
        }

        protected abstract string Subscription { get; }

        protected abstract string Topic { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Starting Subscription for Topic:{Topic} Subscription:{Subscription}");
            RegisterMessageHandler();
            await _subscriptionProcessor.Value.StartProcessingAsync(stoppingToken);
        }

        public void RegisterMessageHandler()
        {
            _logger.LogInformation($"Registering Message Handler for Topic:{Topic} Subscription:{Subscription}");
            _subscriptionProcessor.Value.ProcessMessageAsync += ProcessMessagesAsync;
            _subscriptionProcessor.Value.ProcessErrorAsync += ExceptionReceivedHandler;
        }

        private async Task CloseAsync()
        {
            _logger.LogInformation($"Closing Subscription for Topic:{Topic} Subscription:{Subscription}");
            await _subscriptionProcessor.Value.StopProcessingAsync();
            await _subscriptionProcessor.Value.CloseAsync();
        }

        private ServiceBusProcessor CreateSubscriptionClient()
        {
            var sbc = new ServiceBusClient(_connectionString);

            var messageHandlerOptions = new ServiceBusProcessorOptions()
            {
                MaxConcurrentCalls = 1,
                AutoCompleteMessages = false
            };

            var processor = sbc.CreateProcessor(Topic, Subscription, messageHandlerOptions);
            return processor;
        }

        private async Task ProcessMessagesAsync(ProcessMessageEventArgs args)
        {
            IncomingMessage incomingMessage;
            try
            {
                incomingMessage = _messageFactory.CreateIncomingMessage(args.Message);
            }
            catch (MessageTypeNotSupportedException ex)
            {
                // This will only happen if our Protobuf classes are out of date.
                // There's nothing we can do at this point so log the error and dequeue the message.
                _logger.LogError(ex.Message);
                await args.CompleteMessageAsync(args.Message);
                return;
            }

            Log(incomingMessage);

            // Determine if implements Mediatr INotification interface
            var canBeBroadcast = typeof(INotification).IsAssignableFrom(incomingMessage.PayloadType);

            if (canBeBroadcast)
            {
                // We need to create our own scope because the ServiceBusConsumer is a singleton and
                // the lifecycle of our handlers is different.
                using var serviceScope = _serviceProvider.CreateScope();

                var mediator = serviceScope.ServiceProvider.GetRequiredService<IMediator>();

                await mediator.Publish(incomingMessage.Payload, args.CancellationToken);
            }
            else
            {
                _logger.LogWarning(
                    $"Message received {incomingMessage.PayloadType.FullName} does not implement {nameof(INotification)} and cannot be broadcast. ");
            }

            await args.CompleteMessageAsync(args.Message);
        }

        private Task ExceptionReceivedHandler(ProcessErrorEventArgs processErrorEventArgs)
        {
            _logger.LogError(processErrorEventArgs.Exception, "Message handler encountered an exception");

            _logger.LogDebug($"- Endpoint: {processErrorEventArgs.FullyQualifiedNamespace}");
            _logger.LogDebug($"- Entity Path: {processErrorEventArgs.EntityPath}");
            _logger.LogDebug($"- Executing Action: {processErrorEventArgs.ErrorSource.ToString()}");

            return Task.CompletedTask;
        }

        private void Log(IncomingMessage message)
        {
            const string formattedMessage =
                "[Service Bus Message Received] [MessageId]:{MessageId} {objectType} {objectJson}";

            _logger.LogDebug(formattedMessage, message.PayloadType.Name, message.MessageId,
                JsonConvert.SerializeObject(message.Payload, Formatting.Indented));
        }
    }
}