using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingASubmittedRs7
{
    public class IfTheRequestRollStatusReversesBackIntoDraft : IntegrationTestBase
    {
        private const string Url = "api/rs7";

        public IfTheRequestRollStatusReversesBackIntoDraft(
            RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output,
            TestState testState)
            : base(testSetUp, output, testState)
        {
        }

        protected override void Arrange()
        {
            If
                .A_rs7_has_been_created()
                .An_rs7_is_ready_for_internal_ministry_review()
                .UseResult(result => Rs7Model = result);
        }

        private Rs7Model Rs7Model { get; set; } = new Rs7Model();

        protected override void Act()
        {
            var command = Command.UpdateRs7(Rs7Model, rs7 => rs7.RollStatus = RollStatus.ExternalDraft);
            
            // Act
            Api.Put($"{Url}/{Rs7Model.Id}", command);
        }

        [Fact]
        public void ThenTheResponseShouldBeABadRequest()
        {
            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode(ErrorCode.InvalidRollStatusTransition);
        }
    }
}