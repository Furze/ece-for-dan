using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoE.ECE.Web.Infrastructure.Settings;
using Moe.Library.ServiceBus;

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
        private readonly IMessageResolver _messageResolver;
        private readonly Lazy<SubscriptionClient> _subscriptionClient;

        protected ServiceBusConsumer(
            IOptions<ConnectionStrings> options,
            ILogger<ServiceBusConsumer> logger,
            IServiceProvider serviceProvider, IMessageResolver messageResolver)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _messageResolver = messageResolver;
            _connectionString = options.Value.ServiceBus;
            _subscriptionClient = new Lazy<SubscriptionClient>(CreateSubscriptionClient);
        }

        protected abstract string Subscription { get; }

        protected abstract string Topic { get; }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Starting Subscription for Topic:{Topic} Subscription:{Subscription}");

            RegisterMessageHandler();

            //Do your preparation (e.g. Start code) here
            while (!stoppingToken.IsCancellationRequested) return Task.CompletedTask;

            return CloseAsync();
        }

        public void RegisterMessageHandler()
        {
            _logger.LogInformation($"Registering Message Handler for Topic:{Topic} Subscription:{Subscription}");

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _subscriptionClient.Value.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private Task CloseAsync()
        {
            _logger.LogInformation($"Closing Subscription for Topic:{Topic} Subscription:{Subscription}");
            return _subscriptionClient.Value.CloseAsync();
        }

        private SubscriptionClient CreateSubscriptionClient()
        {
            return new SubscriptionClient(
                _connectionString,
                Topic,
                Subscription,
                ReceiveMode.PeekLock,
                RetryPolicy.Default);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var incomingMessage = new IncomingMessage(message, _messageResolver);

            Log(incomingMessage);

            // Determine if implements Mediatr INotification interface
            var canBeBroadcast = typeof(INotification).IsAssignableFrom(incomingMessage.PayloadType);

            if (canBeBroadcast)
            {
                // We need to create our own scope because the ServiceBusConsumer is a singleton and
                // the lifecycle of our handlers is different.
                using var serviceScope = _serviceProvider.CreateScope();

                var mediator = serviceScope.ServiceProvider.GetService<IMediator>();

                await mediator.Publish(incomingMessage.Payload, token);
            }
            else
            {
                _logger.LogWarning(
                    $"Message received {incomingMessage.PayloadType.FullName} does not implement {nameof(INotification)} and cannot be broadcast. ");
            }

            await _subscriptionClient.Value.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            _logger.LogError(exceptionReceivedEventArgs.Exception, "Message handler encountered an exception");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            _logger.LogDebug($"- Endpoint: {context.Endpoint}");
            _logger.LogDebug($"- Entity Path: {context.EntityPath}");
            _logger.LogDebug($"- Executing Action: {context.Action}");

            return Task.CompletedTask;
        }

        private void Log(IncomingMessage message)
        {
            const string formattedMessage =
                "[Service Bus Message Received] [MessageId]:{MessageId} {objectType} {objectJson}";

            _logger.LogDebug(formattedMessage, message.PayloadType.Name, message.MessageId,
                JsonSerializer.Serialize(message.Payload, new JsonSerializerOptions {WriteIndented = true})
            );
        }
    }
}