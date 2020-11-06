using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingASubmittedRs7
{
    public class IfTheRequestAttemptsToUpdate : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public IfTheRequestAttemptsToUpdate(
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

            UpdateRs7Command = Command.UpdateRs7(Rs7Model);
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        private UpdateRs7 UpdateRs7Command
        {
            get => TestData.SubmitRs7Command;
            set => TestData.SubmitRs7Command = value;
        }

        protected override void Act()
        {
            // Act
            Api.Put($"{Url}/{Rs7Model.Id}", UpdateRs7Command);
        }

        [Fact]
        public void ThenTheResponseShouldBeABadRequest()
        {
            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode(ErrorCode.InvalidRollStatusForUpdate);
        }
    }
}