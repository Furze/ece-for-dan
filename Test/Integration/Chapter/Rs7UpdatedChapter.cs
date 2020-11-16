using System;
using Bard;
using Events.Integration.Protobuf.Workflow;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Event;
using Moe.ECE.Events.Integration;

namespace MoE.ECE.Integration.Tests.Chapter
{
    public class Rs7UpdatedChapter : Chapter<ECEStoryData>
    {
        public Rs7PeerRejectedChapter And_the_rs7_has_been_returned(Action<Returned>? setUpCommand = null)
        {
            return When(context =>
                {
                    var integrationEvent = new Returned
                    {
                        BusinessEntityId = new ProtoBuf.Bcl.Guid(context.StoryData.Rs7Model.BusinessEntityId.GetValueOrDefault()),
                        BusinessEntityType = Constants.BusinessEntityTypes.Rs7
                    };

                    setUpCommand?.Invoke(integrationEvent);
                
                    context.PublishIntegrationEvent(integrationEvent);
                
                    var domainEvent = context.GetDomainEvent<Rs7PeerRejected>();

                    context.StoryData.Rs7Model = domainEvent;
                })
                .ProceedToChapter<Rs7PeerRejectedChapter>();
        }
        
        public EndChapter<ECEStoryData> And_the_rs7_has_been_approved(Action<ApproveRs7>? setUpCommand = null)
        {
            return When(context =>
            {
                var command = new ApproveRs7(context.StoryData.Rs7Model.BusinessEntityId.GetValueOrDefault());

                setUpCommand?.Invoke(command);
                
                context.CqrsExecute(command);
                
                var domainEvent = context.GetDomainEvent<Rs7Approved>();

                context.StoryData.Rs7Model = domainEvent;
            })
                .End();
        }
    }
}