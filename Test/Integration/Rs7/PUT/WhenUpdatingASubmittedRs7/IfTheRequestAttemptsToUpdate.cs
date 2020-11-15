using Bard;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingASubmittedRs7
{
    public class IfTheRequestAttemptsToUpdate : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public IfTheRequestAttemptsToUpdate(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        protected override void Arrange() =>
            Given
                .A_rs7_has_been_created()
                .An_rs7_is_ready_for_internal_ministry_review()
                .GetResult(storyData => Rs7Model = storyData.Rs7Model);

        protected override void Act()
        {
            UpdateRs7? updateRs7Command = ModelBuilder.UpdateRs7(Rs7Model);

            // Act
            When.Put($"{Url}/{updateRs7Command.Id}", updateRs7Command);
        }

        [Fact]
        public void ThenTheResponseShouldBeABadRequest() =>
            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode(ErrorCode.InvalidRollStatusForUpdate);
    }
}