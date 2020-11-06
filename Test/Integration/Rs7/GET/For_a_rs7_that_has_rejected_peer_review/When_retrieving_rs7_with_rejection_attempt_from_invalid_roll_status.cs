using System;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.GET.For_a_rs7_that_has_rejected_peer_review
{
    public class Rs7_has_been_submitted_for_peer_approval: SpeedyIntegrationTestBase
    {
        public Rs7_has_been_submitted_for_peer_approval(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState testState) : base(testSetUp, output, testState)
        {
        }

        private Exception Exception
        {
            get => TestData.Exception;
            set => TestData.Exception = value;
        }

        private Rs7Updated ArrangeResult
        {
            get => TestData.ArrangeResult;
            set => TestData.ArrangeResult = value;
        }

        protected override void Arrange()
        { 
            If.A_rs7_has_been_created()
                .And().The()
                .Rs7_has_been_submitted_for_peer_approval()
                .And().The()
                .Rs7_Has_Been_Peer_Approved()
                .UseResult(x => ArrangeResult = x);
        }

        protected override void Act()
        {
            try
            {
                RejectPeerReview();
            }
            catch (Exception e)
            {
                this.Exception = e;
            }

            Api.Get($"{Constants.Url}/{ArrangeResult.Id}");
        }

        private void RejectPeerReview()
        {
            var command = Command.Rs7PeerReject((Guid) ArrangeResult.BusinessEntityId!);
            var cqrs = Services.GetService<ICqrs>();
            AsyncHelper.RunSync(() => cqrs.ExecuteAsync(command));
        }
        
        [Fact]
        public void Exception_should_be_thrown_when_processing_message()
        {
            this.Exception.ShouldNotBeNull();
        }
        
        [Fact]
        public void Roll_status_should_not_transition_to_rejected_state()
        {
          Then.TheResponse.Content<Rs7Model>().RollStatus.ShouldBe(RollStatus.InternalReadyForReview);
        }

        [Fact]
        public void Rejected_domain_event_should_not_be_raised()
        {
            Then.A_domain_event_should_not_be_fired<Rs7PeerRejected>();
        }
    }
}