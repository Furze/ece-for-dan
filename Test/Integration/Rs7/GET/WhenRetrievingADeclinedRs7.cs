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
    public class WhenRetrievingADeclinedRs7 : SpeedyIntegrationTestBase
    {
        public WhenRetrievingADeclinedRs7(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        protected override void Arrange()
        {
            Given
                .A_rs7_skeleton_has_been_created(setup => setup.FundingPeriodYear = 2020)
                .The_rs7_has_been_submitted_for_peer_approval()
                .The_rs7_has_been_peer_approved()
                .The_rs7_has_been_declined()
                .GetResult(storyData => Rs7Model = storyData.Rs7Model);
        }

        protected override void Act()
        {
            When.Get($"{Url}/{Rs7Model.Id}");
        }

        [Fact]
        public void ThenTheRollStatusIsUpdatedToDeclined()
        {
            Then
                .Response.Content<Rs7Model>().RollStatus.ShouldBe(RollStatus.Declined);
        }
        
        [Fact]
        public void Then_the_response_snapshot_should_be_ok()
        {
            Then.Snapshot().Match<Rs7Model>(IgnoreFieldsFor.Rs7Model);
        }
    }
}