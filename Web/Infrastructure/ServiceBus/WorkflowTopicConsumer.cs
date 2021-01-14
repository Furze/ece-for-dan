using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moe.ECE.Events.Integration;
using MoE.ECE.Web.Infrastructure.Settings;
using Moe.Library.ServiceBus;

namespace MoE.ECE.Web.Infrastructure.ServiceBus
{
    public class WorkflowTopicConsumer : ServiceBusConsumer
    {
        public WorkflowTopicConsumer(IOptions<ConnectionStrings> options, ILogger<ServiceBusConsumer> logger, IServiceProvider serviceProvider, IMessageResolver messageResolver) : base(options, logger, serviceProvider, messageResolver)
        {
        }

        protected override string Subscription => Constants.Subscription.ECE;

        protected override string Topic => Constants.Topic.Workflow;
    }
}