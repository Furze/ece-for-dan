using Bard;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenSubmittingForApproval
{
    public class IfTheRequestIsSubmittedTwice : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public IfTheRequestIsSubmittedTwice(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private Rs7Model Rs7
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        protected override void Arrange() =>
            Given
                .A_rs7_has_been_created()
                .rs7_submitted_for_approval()
                .GetResult(created => Rs7 = created.Rs7Model);

        [Fact]
        public void IfWeTryToSubmitTwice()
        {
            // Arrange
            SubmitRs7ForApproval? submitForApproval = ModelBuilder.SubmitRs7ForApproval(Rs7);

            // Act
            When.Post($"{Url}/{Rs7.Id}/submissions-for-approval", submitForApproval);

            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode(ErrorCode.InvalidRollStatusForSubmitRs7ForApproval);
        }
    }
}