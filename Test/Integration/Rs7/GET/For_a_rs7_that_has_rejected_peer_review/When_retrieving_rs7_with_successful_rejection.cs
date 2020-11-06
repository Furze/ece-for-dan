using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.GET.For_a_rs7_that_has_rejected_peer_review
{
    public class When_retrieving_rs7_with_successful_rejection: SpeedyIntegrationTestBase
    {
        public When_retrieving_rs7_with_successful_rejection(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState testState) : base(testSetUp, output, testState)
        {
        }
        
        private Rs7Updated ArrangeResult
        {
            get => TestData.ArrangeResult;
            set => TestData.ArrangeResult = value;
        }
        
        private Rs7Updated ActResult
        {
            get => TestData.ActResult;
            set => TestData.ActResult = value;
        }

        protected override void Arrange()
        {
            If.A_rs7_has_been_created()
                .And().The()
                .Rs7_has_been_submitted_for_peer_approval()
                .And().Then().The()
                .Rs7_has_been_peer_review_rejected()
                .UseResult(x => this.ArrangeResult = x);
        }

        protected override void Act()
        {
            Api.Get($"{Constants.Url}/{ArrangeResult.Id}");
        }

        [Fact]
        public void Roll_status_has_been_updated()
        {
          Then.TheResponse.Content<Rs7Model>().RollStatus.ShouldBe(RollStatus.ExternalReturnedForEdit);
        }

        [Fact]
        public void Domain_event_raised()
        {
            Then.A_domain_event_should_be_fired<Rs7PeerRejected>();
        }
    }
}