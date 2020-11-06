using Bard;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
{
    public class IfTheRequestHasNoAdvancedDayData : SpeedyIntegrationTestBase
    {
        public IfTheRequestHasNoAdvancedDayData(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
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

        protected override void Act()
        {
            // Act
            When.Put($"{Url}/{Rs7.Id}", ModelBuilder.UpdateRs7(Rs7, rs7 =>
            {
                rs7.RollStatus = RollStatus.InternalReadyForReview;
                if (rs7.AdvanceMonths == null) return;

                foreach (var rs7AdvanceMonth in rs7.AdvanceMonths)
                {
                    rs7AdvanceMonth.Sessional = null;
                    rs7AdvanceMonth.AllDay = null;
                    rs7AdvanceMonth.ParentLed = null;
                }
            }));
        }

        /// <summary>
        ///     This is a valid scenario just checking that we don't trip up our max min validation.
        /// </summary>
        [Fact]
        public void ThenTheResponseShouldBeNoContent()
        {
            Then.Response
                .ShouldBe
                .NoContent();
        }
    }
}