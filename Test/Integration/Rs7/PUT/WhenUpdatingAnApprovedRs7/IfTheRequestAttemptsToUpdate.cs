using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingAnApprovedRs7
{
    public class IfTheRequestAttemptsToUpdate : IntegrationTestBase
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
                .And_the_rs7_has_been_approved()
                .UseResult(result => Rs7Model = result);
        }

        private Rs7Model Rs7Model { get; set; } = new Rs7Model();

        protected override void Act()
        {
            var command = Command.UpdateRs7(Rs7Model, rs7 => rs7.IsAttested = !rs7.IsAttested);
            
            // Act
            Api.Put($"{Url}/{Rs7Model.Id}", command);
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