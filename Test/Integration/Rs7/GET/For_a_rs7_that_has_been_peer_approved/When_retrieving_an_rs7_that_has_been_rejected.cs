using Bard;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.GET.For_a_rs7_that_has_been_peer_approved
{
    // ReSharper disable once InconsistentNaming
    public class When_retrieving_an_rs7_that_has_been_rejected : SpeedyIntegrationTestBase
    {
        public When_retrieving_an_rs7_that_has_been_rejected(RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output,
            testState)
        {
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created()
                .The_rs7_has_been_submitted_for_peer_approval()
                .The_rs7_has_been_peer_review_rejected()
                .GetResult(storyData => Rs7Model = storyData.Rs7Model);
        }

        protected override void Act()
        {
            When.Get($"{Constants.Url}/{Rs7Model.Id}");
        }

        [Fact]
        public void Domain_event_raised()
        {
            A_domain_event_should_be_fired<Rs7PeerRejected>();
        }

        [Fact]
        public void Roll_status_has_been_updated()
        {
            Then.Response.Content<Rs7Model>().RollStatus.ShouldBe(RollStatus.ExternalReturnedForEdit);
        }
    }
}