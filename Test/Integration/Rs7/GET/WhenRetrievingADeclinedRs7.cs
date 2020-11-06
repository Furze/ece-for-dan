using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.GET
{
    public class WhenRetrievingADeclinedRs7 : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";
        private int _rs7Id;
        public WhenRetrievingADeclinedRs7(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState testState) : base(testSetUp, output, testState)
        {
        }
        
        protected override void Arrange()
        {
            If
                .A_rs7_has_been_created()
                .Rs7_has_been_submitted_for_peer_approval()
                .Rs7_Has_Been_Peer_Approved()
                .Rs7_has_been_declined()
                .UseResult(result => _rs7Id = result.Id);
        }

        protected override void Act()
        {
            Api.Get($"{Url}/{_rs7Id}");
        }

        [Fact]
        public void ThenTheRollStatusIsUpdatedToPendingApproval()
        {
            Then
                .TheResponse.Content<Rs7Model>().RollStatus.ShouldBe(RollStatus.Declined);
        }
    }
}