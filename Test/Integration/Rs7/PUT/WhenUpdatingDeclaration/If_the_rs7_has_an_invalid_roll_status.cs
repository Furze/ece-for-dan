using Bard;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingDeclaration
{
    public class If_the_rs7_has_an_invalid_roll_status : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public If_the_rs7_has_an_invalid_roll_status(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
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
                .GetResult(storyData => Rs7Model = storyData.Rs7Model);

        [Fact]
        public void Invalid_roll_status()
        {
            UpdateRs7Declaration? updateRs7Declaration = ModelBuilder.UpdateRs7Declaration();

            When.Put($"{Url}/{Rs7Model.Id}/declaration", updateRs7Declaration);

            Then.Response.ShouldBe.BadRequest.WithErrorCode(ErrorCode.InvalidRollStatusForUpdate);
        }
    }
}