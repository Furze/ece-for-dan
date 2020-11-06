using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenSubmittingForApproval
{
    public class IfTheRequestIsContainsAnInvalidEntitlementMonth : SpeedyIntegrationTestBase
    {
        public IfTheRequestIsContainsAnInvalidEntitlementMonth(
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
                command.EntitlementMonths.First().MonthNumber = 6;
            });

            // Act
            Api.Post($"{Url}/{Rs7.Id}/submissions-for-approval", submitForApproval);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode("1030");
        }
        
        [Fact]
        public void IfTheYearIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var submitForApproval = Command.SubmitRs7ForApproval(Rs7, command => 
            {
                command.EntitlementMonths.First().Year = 2021;
            });

            // Act
            Api.Post($"{Url}/{Rs7.Id}/submissions-for-approval", submitForApproval);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode("1030");
        }
    }
}