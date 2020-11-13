using System.Linq;
using Bard;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenSubmittingForApproval
{
    public class IfTheRequestIsContainsAnInvalidAdvanceMonths : SpeedyIntegrationTestBase
    {
        public IfTheRequestIsContainsAnInvalidAdvanceMonths(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created()
                .GetResult(created => Rs7 = created.Rs7Model);
        }

        private Rs7Model Rs7
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10000)]
        public void IfTheYearIsOutsideTheAllowedValueThenTheResponseShouldBeAHttp400(int year)
        {
            // Arrange
            var submitForApproval = ModelBuilder.SubmitRs7ForApproval(Rs7, command =>
            {
                if (command.AdvanceMonths != null)
                    command.AdvanceMonths.First().Year = year;
            });

            // Act
            When.Post($"{Url}/{Rs7.Id}/submissions-for-approval", submitForApproval);

            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode(ErrorCode.InclusiveBetweenValidator);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(13)]
        public void IfTheMonthIsOutsideTheAllowedValueThenTheResponseShouldBeAHttp400(int month)
        {
            // Arrange
            var submitForApproval = ModelBuilder.SubmitRs7ForApproval(Rs7, command =>
            {
                if (command.AdvanceMonths != null)
                    command.AdvanceMonths.First().MonthNumber = month;
            });

            // Act
            When.Post($"{Url}/{Rs7.Id}/submissions-for-approval", submitForApproval);

            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode(ErrorCode.InclusiveBetweenValidator);
        }

        [Fact]
        public void IfAttestedIsNullThenResponseShouldBeAHttp400()
        {
            // Arrange
            var submitForApproval = ModelBuilder.SubmitRs7ForApproval(Rs7, command => { command.IsAttested = null; });

            // Act
            When.Post($"{Url}/{Rs7.Id}/submissions-for-approval", submitForApproval);

            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode("PredicateValidator");
        }

        [Fact]
        public void IfTheMonthIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var submitForApproval = ModelBuilder.SubmitRs7ForApproval(Rs7, command =>
            {
                if (command.AdvanceMonths != null)
                    command.AdvanceMonths.First().MonthNumber = 12;
            });

            // Act
            When.Post($"{Url}/{Rs7.Id}/submissions-for-approval", submitForApproval);

            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode("1020");
        }

        [Fact]
        public void IfTheYearIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var submitForApproval = ModelBuilder.SubmitRs7ForApproval(Rs7, command =>
            {
                if (command.AdvanceMonths != null)
                    command.AdvanceMonths.First().Year = 2021;
            });

            // Act
            When.Post($"{Url}/{Rs7.Id}/submissions-for-approval", submitForApproval);

            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode("1020");
        }
    }
}