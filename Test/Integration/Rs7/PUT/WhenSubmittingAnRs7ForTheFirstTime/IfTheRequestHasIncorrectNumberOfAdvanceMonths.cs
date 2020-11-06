using System.Linq;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
{
    public class IfTheRequestHasIncorrectNumberOfAdvanceMonths : SpeedyIntegrationTestBase
    {
        public IfTheRequestHasIncorrectNumberOfAdvanceMonths(
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
                rs7.AdvanceMonths = Enumerable.Range(6, 3)
                    .Select(month => new Rs7AdvanceMonthModel
                    {
                        MonthNumber = month
                    }).ToList();
            }));
        }

        [Fact]
        public void ThenTheResponseShouldBeAHttp400()
        {
            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<UpdateRs7>(rs7 => rs7.AdvanceMonths)
                .WithMessage("A total of 4 months must be provided.");
        }
    }
}