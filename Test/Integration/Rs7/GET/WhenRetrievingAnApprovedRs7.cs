using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.GET
{
    public class WhenRetrievingAnApprovedRs7 : IntegrationTestBase
    {
        private const string Url = "api/rs7";
        private int _rs7Id;

        public WhenRetrievingAnApprovedRs7(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState testState) : base(testSetUp, output, testState)
        {
        }

        protected override void Arrange()
        {
            If
                .A_rs7_has_been_created()
                .An_rs7_is_ready_for_internal_ministry_review()
                .And_the_rs7_has_been_approved()
                .UseResult(result => _rs7Id = result.Id);
        }

        protected override void Act()
        {
            Api.Get($"{Url}/{_rs7Id}");
        }

        [Fact]
        public void ThenTheStatusShouldBeCorrect()
        {
            Then
                .TheResponse
                .Content<Rs7Model>()
                .RollStatus
                .ShouldBe(RollStatus.InternalApproved);
        }
    }
}