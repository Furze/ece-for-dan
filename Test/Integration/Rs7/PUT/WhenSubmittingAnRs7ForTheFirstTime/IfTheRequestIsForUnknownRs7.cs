using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
{
    public class IfTheRequestIsForUnknownRs7 : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";
        private const int SomeRandomRs7Id = 53232;

        public IfTheRequestIsForUnknownRs7(
            RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output,
            TestState testState)
            : base(testSetUp, output, testState)
        {
        }

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
            Api.Put($"{Url}/{SomeRandomRs7Id}", Command.UpdateRs7(Rs7));
        }

        [Fact]
        public void ThenTheResponseShouldBeAHttp404()
        {
            Then.TheResponse
                .ShouldBe
                .NotFound();
        }
    }
}