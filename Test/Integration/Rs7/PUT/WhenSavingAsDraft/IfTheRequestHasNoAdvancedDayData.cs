using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSavingAsDraft
{
    public class IfTheRequestHasNoAdvancedDayData : SpeedyIntegrationTestBase
    {
        public IfTheRequestHasNoAdvancedDayData(
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
        
        protected override void Act()
        {
            // Act
            Api.Put($"{Url}/{Rs7.Id}", Command.UpdateRs7(Rs7, rs7 =>
            {
                rs7.RollStatus = RollStatus.InternalReadyForReview;
                if (rs7.AdvanceMonths != null)
                    foreach (var rs7AdvanceMonth in rs7.AdvanceMonths)
                    {
                        rs7AdvanceMonth.Sessional = null;
                        rs7AdvanceMonth.AllDay = null;
                        rs7AdvanceMonth.ParentLed = null;
                    }
            }));
        }

        /// <summary>
        /// This is a valid scenario just checking that we don't trip up our max min validation.
        /// </summary>
        [Fact]
        public void ThenTheResponseShouldBeNoContent()
        {
            Then.TheResponse
                .ShouldBe
                .NoContent();
        }
    }
}