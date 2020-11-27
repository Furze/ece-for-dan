using System;
using Bard;
using MoE.ECE.Domain.Command.Rs7;

namespace MoE.ECE.Integration.Tests.Chapter
{
    public class Rs7PeerRejectedChapter : Chapter<ECEStoryData>
    {
        public Rs7UpdatedChapter An_rs7_is_ready_for_internal_ministry_review(Action<UpdateRs7>? setUpCommand = null)
        {
            return When(context => Rs7Stories.AnRs7IsReadyForInternalMinistryReview(context, setUpCommand))
                .ProceedToChapter<Rs7UpdatedChapter>();
        }
    }
}