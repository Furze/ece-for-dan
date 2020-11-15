using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoE.ECE.Web.Infrastructure.ServiceBus;
using MoE.ECE.Web.Infrastructure.Settings;
using Moe.Library.ServiceBus;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    public class InMemoryServiceBus : ServiceBusPublisher
    {
        public InMemoryServiceBus(IOptions<ConnectionStrings> options, ILogger<ServiceBusConsumer> logger) : base(
            options, logger)
        {
        }

        /// <summary>
        ///     Override the actual Send so we don't really publish anything...
        /// </summary>
        /// <param name="outgoingMessage"></param>
        /// <returns></returns>
        protected override Task SendAsync(OutgoingMessage outgoingMessage) => Task.CompletedTask;
    }
}