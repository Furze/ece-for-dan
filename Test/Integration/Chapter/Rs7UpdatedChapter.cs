using System;
using Bard;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Event;

namespace MoE.ECE.Integration.Tests.Chapter
{
    public class Rs7UpdatedChapter : Chapter<ECEStoryData>
    {
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