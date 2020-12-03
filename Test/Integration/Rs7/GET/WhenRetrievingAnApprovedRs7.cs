using Bard;
using Events.Integration.Protobuf.Roll;
using Events.Integration.Protobuf.Workflow;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using RollStatus = MoE.ECE.Domain.Model.ValueObject.RollStatus;

namespace MoE.ECE.Integration.Tests.Rs7.GET
{
    public class WhenRetrievingAnApprovedRs7 : IntegrationTestBase<ECEStoryBook, ECEStoryData>
    {
        public WhenRetrievingAnApprovedRs7(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";
        private int _rs7Id;

        protected override void Arrange() =>
            Given
                .A_rs7_has_been_created()
                .An_rs7_is_ready_for_internal_ministry_review()
                .And_the_rs7_has_been_approved()
                .GetResult(storyData => _rs7Id = storyData.Rs7Model.Id);

        protected override void Act() => When.Get($"{Url}/{_rs7Id}");
        
        [Fact]
        public void ThenTheStatusShouldBeCorrect() =>
            Then
                .Response
                .Content<Rs7Model>()
                .RollStatus
                .ShouldBe(RollStatus.InternalApproved);
        
        [Fact]
        public void Then_the_response_snapshot_should_be_ok() =>
            Then.Snapshot().Match<Rs7Model>(IgnoreFieldsFor.Rs7Model);

        [Fact]
        public void Then_an_Rs7Updated_integration_event_should_be_fired() =>
            An_integration_event_should_be_fired<Rs7Updated>();
        
        [Fact]
        public void Then_an_FundingEntitlementUpdated_integration_event_should_be_fired() =>
            An_integration_event_should_be_fired<FundingEntitlementUpdated>();
    }
}