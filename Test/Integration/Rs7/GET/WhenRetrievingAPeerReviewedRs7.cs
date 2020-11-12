using Bard;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.GET
{
    public class WhenRetrievingAPeerReviewedRs7 : SpeedyIntegrationTestBase
    {
        public WhenRetrievingAPeerReviewedRs7(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";
        private int _rs7Id;

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created()
                .The_rs7_has_been_submitted_for_peer_approval()
                .The_rs7_has_been_peer_approved()
                .GetResult(storyData => _rs7Id = storyData.Rs7Model.Id);
        }

        protected override void Act()
        {
            When.Get($"{Url}/{_rs7Id}");
        }

        [Fact]
        public void ThenTheRollStatusIsUpdatedToPendingApproval()
        {
            Then
                .Response.Content<Rs7Model>().RollStatus.ShouldBe(RollStatus.InternalReadyForReview);
        }
    }
}