using System.Linq;
using Bard;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
{
    public class IfTheAdvancedDayAllDayAmountExceedsTheMaximumAmountForTheMonth : SpeedyIntegrationTestBase
    {
        public IfTheAdvancedDayAllDayAmountExceedsTheMaximumAmountForTheMonth(RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output,
            testState)
        {
        }

        private const string Url = "api/rs7";

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created()
                .GetResult(created => Rs7 = created.Rs7Created);
        }

        private Rs7Model Rs7
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        /// <summary>
        ///     Service is Montesorri Little Hands which is Education & Care Service service type.
        ///     It only allows AllDay Advanced Days.
        ///     For the July 2020 the max number of AllDay allowed is 23
        /// </summary>
        [Fact]
        public void IfASessionalAmountGreaterThanTheMaxNumberIsProvidedThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7 = ModelBuilder.UpdateRs7(Rs7, rs7 =>
            {
                if (rs7.AdvanceMonths == null) return;
                
                var month = rs7.AdvanceMonths.First(model =>
                    model.MonthNumber == CalendarMonth.July.Id && model.Year == 2020);

                month.AllDay = 24;
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateRs7);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7AdvanceMonthModel>(model => model.AllDay)
                .WithErrorCode(ErrorCode.LessThanOrEqualValidator);
        }
    }
}