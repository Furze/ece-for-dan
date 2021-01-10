using System.Linq;
using Bard;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
{
    public class IfTheRequestIsContainsAnInvalidEntitlementMonth : SpeedyIntegrationTestBase
    {
        public IfTheRequestIsContainsAnInvalidEntitlementMonth(RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output,
            testState)
        {
        }

        private const string Url = "api/rs7";

        protected override void Arrange()
        {
            Given
                .A_rs7_skeleton_has_been_created(setup => setup.FundingPeriodYear = 2020)
                .GetResult(created => Rs7 = created.Rs7Model);
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
            var updateRs7 = ModelBuilder.UpdateRs7(Rs7, rs7 =>
            {
                if (rs7.EntitlementMonths != null) rs7.EntitlementMonths.First().MonthNumber = 6;
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateRs7);

            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode("1030");
        }

        [Fact]
        public void IfTheYearIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7 = ModelBuilder.UpdateRs7(Rs7, rs7 =>
            {
                if (rs7.EntitlementMonths != null) rs7.EntitlementMonths.First().Year = 2021;
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateRs7);

            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode("1030");
        }
    }
}