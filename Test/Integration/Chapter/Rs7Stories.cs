using System;
using Bard;
using Events.Integration.Protobuf.Eli;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Integration.Tests.Infrastructure;

namespace MoE.ECE.Integration.Tests.Chapter
{
    public class Rs7Stories
    {
        public static void AnRs7HasBeenReceivedExternally(ScenarioContext<ECEStoryData> context,
            Rs7Received integrationEvent)
        {
            context.PublishIntegrationEvent(integrationEvent);

            context.StoryData.Rs7Model = context.GetDomainEvent<FullRs7Created>();
        }

        public static void AnRs7IsReadyForInternalMinistryReview(ScenarioContext<ECEStoryData> context,
            Action<UpdateRs7>? setUpCommand)
        {
            var command = ModelBuilder.UpdateRs7(context.StoryData.Rs7Model,
                rs7 => { rs7.RollStatus = RollStatus.InternalReadyForReview; });

            setUpCommand?.Invoke(command);

            context.CqrsExecute(command);

            context.StoryData.Rs7Model = context.GetDomainEvent<Rs7Updated>();
        }
    }
}