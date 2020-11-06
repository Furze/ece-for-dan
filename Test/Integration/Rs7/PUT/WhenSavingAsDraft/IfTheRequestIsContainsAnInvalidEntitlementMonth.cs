using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSavingAsDraft
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
            var updateRs7 = Command.SaveAsDraft(Rs7, rs7 => 
            {
                rs7.EntitlementMonths.First().MonthNumber = 6;
            });

            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateRs7);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode("1030");
        }
        
        [Fact]
        public void IfTheYearIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7 = Command.SaveAsDraft(Rs7, rs7 => 
            {
                rs7.EntitlementMonths.First().Year = 2021;
            });

            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateRs7);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode("1030");
        }
    }
}