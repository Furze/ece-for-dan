using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moe.ECE.Events.Integration;
using MoE.ECE.Web.Infrastructure.Settings;

namespace MoE.ECE.Web.Infrastructure.ServiceBus
{
    public class WorkflowTopicConsumer : ServiceBusConsumer
    {
        public WorkflowTopicConsumer(IOptions<ConnectionStrings> options, ILogger<ServiceBusConsumer> logger, IServiceProvider serviceProvider) : base(options, logger, serviceProvider)
        {
        }

        protected override string Subscription => Constants.Subscription.ECE;

        protected override string Topic => Constants.Topic.Workflow;
    }
}