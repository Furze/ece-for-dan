using System.Linq;
using Bard;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
{
    public class IfTheRequestHasIncorrectNumberOfEntitlementMonths : SpeedyIntegrationTestBase
    {
        public IfTheRequestHasIncorrectNumberOfEntitlementMonths(RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output,
            testState)
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

        protected override void Act()
        {
            // Act
            When.Put($"{Url}/{Rs7.Id}", ModelBuilder.UpdateRs7(Rs7, rs7 =>
            {
                rs7.EntitlementMonths = Enumerable.Range(6, 3)
                    .Select(month => new Rs7EntitlementMonthModel
                    {
                        MonthNumber = month
                    }).ToArray();
            }));
        }

        [Fact]
        public void ThenTheResponseShouldBeAHttp400()
        {
            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<UpdateRs7>(rs7 => rs7.EntitlementMonths)
                .WithMessage("A total of 4 months must be provided.");
        }
    }
}