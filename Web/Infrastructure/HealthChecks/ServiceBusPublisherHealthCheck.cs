using System;
using System.Threading;
using System.Threading.Tasks;
using App.Metrics.Health;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Moe.ECE.Events.Integration;
using MoE.ECE.Web.Infrastructure.Settings;


namespace MoE.ECE.Web.Infrastructure.HealthChecks
{
    public class ServiceBusPublisherHealthCheck : HealthCheck
    {
        private readonly string _connectionStringFactory;
        private const string HealthCheckName = "ServiceBus Health Check";

        public ServiceBusPublisherHealthCheck(IOptions<ConnectionStrings> connectionString) : base(HealthCheckName)
        {
            _connectionStringFactory = connectionString.Value.ServiceBus;
        }
        
        protected override async ValueTask<HealthCheckResult> CheckAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var sbc = new ServiceBusClient(_connectionStringFactory);
                var sender = sbc.CreateSender(Constants.Topic.ECE);
                var messageDate = DateTime.Now.AddDays(7);
                var scheduledMessage = await sender.ScheduleMessageAsync(new ServiceBusMessage("TEST MESSAGE"),
                    messageDate, cancellationToken);
                await sender.CancelScheduledMessageAsync(scheduledMessage, cancellationToken);
                return HealthCheckResult.Healthy();
            }
            catch (Exception exception)
            {
                return HealthCheckResult.Unhealthy(exception);
            }
        }
    }
}