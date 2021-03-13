using System;
using Events.Integration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoE.ECE.Web.Infrastructure.Settings;
using Moe.Library.ServiceBus;

namespace MoE.ECE.Web.Infrastructure.ServiceBus
{
    public class EliTopicConsumer : ServiceBusConsumer
    {
        public EliTopicConsumer(IOptions<ConnectionStrings> options, ILogger<ServiceBusConsumer> logger, 
            IServiceProvider serviceProvider, MessageFactory messageFactory) 
            : base(options, logger, serviceProvider, messageFactory)
        {
        }

        protected override string Subscription => Constants.Subscription.ECE;

        protected override string Topic => Constants.Topic.ELI;
    }
}
