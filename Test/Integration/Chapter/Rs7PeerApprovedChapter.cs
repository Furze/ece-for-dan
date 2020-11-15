using Bard;
using Events.Integration.Protobuf.Workflow;
using MoE.ECE.Domain.Event;
using Moe.ECE.Events.Integration;
using ProtoBuf.Bcl;

namespace MoE.ECE.Integration.Tests.Chapter
{
    public class Rs7PeerApprovedChapter : Chapter<ECEStoryData>
    {
        public EndChapter<ECEStoryData> The_rs7_has_been_declined()
        {
            return When(context =>
            {
                var declined = new Declined
                {
                    BusinessEntityId = new Guid(context.StoryData.Rs7Model.BusinessEntityId.GetValueOrDefault()),
                    BusinessEntityType = Constants.BusinessEntityTypes.Rs7
                };

                context.PublishIntegrationEvent(declined);

                context.StoryData.Rs7Model = context.GetDomainEvent<Rs7Declined>();
            }).End();
        }
    }
}