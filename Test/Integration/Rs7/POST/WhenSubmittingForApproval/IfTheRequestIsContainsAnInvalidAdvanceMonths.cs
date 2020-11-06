using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenSubmittingForApproval
{
    public class IfTheRequestIsContainsAnInvalidAdvanceMonths : SpeedyIntegrationTestBase
    {
        public IfTheRequestIsContainsAnInvalidAdvanceMonths(
            RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output,
            TestState testState)
            : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

        protected override void Arrange()
        {
            If
                .A_rs7_has_been_created()
                .UseResult(created => Rs7 = created);
        }

        private Rs7Model Rs7
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        [Fact]
        public void IfTheMonthIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var submitForApproval = Command.SubmitRs7ForApproval(Rs7, command => 
            {
                command.AdvanceMonths.First().MonthNumber = 12;
            });
            
            // Act
            Api.Post($"{Url}/{Rs7.Id}/submissions-for-approval", submitForApproval);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode("1020");
        }
        
        [Fact]
        public void IfAttestatedIsNullThenResponseShouldBeAHttp400()
        {
            // Arrange
            var submitForApproval = Command.SubmitRs7ForApproval(Rs7, command =>
            {
                command.IsAttested = null;
            });
            
            // Act
            Api.Post($"{Url}/{Rs7.Id}/submissions-for-approval", submitForApproval);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode("PredicateValidator");
        }
        
        [Fact]
        public void IfTheYearIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var submitForApproval = Command.SubmitRs7ForApproval(Rs7, command => 
            {
                command.AdvanceMonths.First().Year = 2021;
            });

            // Act
            Api.Post($"{Url}/{Rs7.Id}/submissions-for-approval", submitForApproval);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode("1020");
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(10000)]
        public void IfTheYearIsOutsideTheAllowedValueThenTheResponseShouldBeAHttp400(int year)
        {
            // Arrange
            var submitForApproval = Command.SubmitRs7ForApproval(Rs7, command => 
            {
                command.AdvanceMonths.First().Year = year;
            });            

            // Act
            Api.Post($"{Url}/{Rs7.Id}/submissions-for-approval", submitForApproval);

            Then.TheResponse
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
            var submitForApproval = Command.SubmitRs7ForApproval(Rs7, command => 
            {
                command.AdvanceMonths.First().MonthNumber = month;
            });

            // Act
            Api.Post($"{Url}/{Rs7.Id}/submissions-for-approval", submitForApproval);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode(ErrorCode.InclusiveBetweenValidator);
        }
    }
}