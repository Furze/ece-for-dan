using Bard;
using MoE.ECE.Domain.Event;
using Moe.ECE.Events.Integration.ELI;

namespace MoE.ECE.Integration.Tests.Chapter
{
    public class Rs7Stories
    {
        public static void AnRs7HasBeenReceivedExternally(ScenarioContext<ECEStoryData> context,
            Rs7Received integrationEvent)
        {
            context.PublishIntegrationEvent(integrationEvent);

            context.StoryData.Rs7Model = context.GetDomainEvent<Rs7CreatedFromExternal>();
        }
    }
}