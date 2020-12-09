using Bard;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingAnApprovedRs7
{
    public class IfTheRequestAttemptsToUpdate : IntegrationTestBase<ECEStoryBook, ECEStoryData>
    {
        public IfTheRequestAttemptsToUpdate(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

        protected override void Arrange()
        {
            Given
                .A_rs7_skeleton_has_been_created()
                .An_rs7_is_ready_for_internal_ministry_review()
                .And_the_rs7_has_been_approved()
                .GetResult(result => Rs7Model = result.Rs7Model);
        }

        private Rs7Model Rs7Model { get; set; } = new Rs7Model();

        protected override void Act()
        {
            var command = ModelBuilder.UpdateRs7(Rs7Model, rs7 => rs7.IsAttested = !rs7.IsAttested);

            // Act
            When.Put($"{Url}/{Rs7Model.Id}", command);
        }

        [Fact]
        public void ThenTheResponseShouldBeABadRequest()
        {
            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode(ErrorCode.InvalidRollStatusForUpdate);
        }
    }
}