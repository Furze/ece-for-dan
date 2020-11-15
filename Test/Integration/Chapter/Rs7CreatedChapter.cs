using System;
using Bard;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Infrastructure;

namespace MoE.ECE.Integration.Tests.Chapter
{
    public class Rs7CreatedChapter : Chapter<ECEStoryData>
    {
        public Rs7UpdatedChapter And_the_rs7_has_been_saved_as_draft(Action<UpdateRs7>? setUpCommand = null)
        {
            return When(context =>
                {
                    var command = ModelBuilder.UpdateRs7(context.StoryData.Rs7Model, rs7 =>
                    {
                        rs7.RollStatus = RollStatus.ExternalDraft;
                        rs7.Declaration = new DeclarationModel();
                    });

                    setUpCommand?.Invoke(command);

                    context.CqrsExecute(command);

                    context.StoryData.Rs7Model = context.GetDomainEvent<Rs7Updated>();
                })
                .ProceedToChapter<Rs7UpdatedChapter>();
        }

        public EndChapter<ECEStoryData> rs7_submitted_for_approval()
        {
            return When(context =>
            {
                var submitForApproval = ModelBuilder.SubmitRs7ForApproval(context.StoryData.Rs7Model);

                context.CqrsExecute(submitForApproval);
            }).End();
        }

        public Rs7UpdatedChapter An_rs7_is_ready_for_internal_ministry_review(Action<UpdateRs7>? setUpCommand = null)
        {
            return When(context =>
            {
                var command = ModelBuilder.UpdateRs7(context.StoryData.Rs7Model,
                    rs7 => { rs7.RollStatus = RollStatus.InternalReadyForReview; });

                setUpCommand?.Invoke(command);

                context.CqrsExecute(command);

                context.StoryData.Rs7Model = context.GetDomainEvent<Rs7Updated>();
            }).ProceedToChapter<Rs7UpdatedChapter>();
        }

        public Rs7SubmittedForApprovalChapter The_rs7_has_been_submitted_for_peer_approval(Action<SubmitRs7ForApproval>? setUpCommand = null)
        {
            return When(context =>
            {
                var command = ModelBuilder.SubmitRs7ForApproval(context.StoryData.Rs7Model);

                setUpCommand?.Invoke(command);

                context.CqrsExecute(command);

                var domainEVent = context.GetDomainEvent<Rs7SubmittedForApproval>();

                context.StoryData.Rs7Model = domainEVent;
            })
                .ProceedToChapter<Rs7SubmittedForApprovalChapter>();
        }
    }
}