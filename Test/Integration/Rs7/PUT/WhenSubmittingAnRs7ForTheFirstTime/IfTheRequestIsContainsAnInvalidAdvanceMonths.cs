using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
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
            var updateRs7 = Command.UpdateRs7(Rs7, rs7 => 
            {
                rs7.AdvanceMonths.First().MonthNumber = 12;
            });
            
            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateRs7);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode("1020");
        }
        
        [Fact]
        public void IfTheYearIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7 = Command.UpdateRs7(Rs7, rs7 => 
            {
                rs7.AdvanceMonths.First().Year = 2021;
            });

            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateRs7);

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
            var updateRs7 = Command.UpdateRs7(Rs7, rs7 =>
            {
                rs7.AdvanceMonths.First().Year = year;
            });            

            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateRs7);

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
            var updateRs7 = Command.UpdateRs7(Rs7, rs7 =>
            {
                rs7.AdvanceMonths.First().MonthNumber = month;
            });

            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateRs7);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode(ErrorCode.InclusiveBetweenValidator);
        }
    }
}