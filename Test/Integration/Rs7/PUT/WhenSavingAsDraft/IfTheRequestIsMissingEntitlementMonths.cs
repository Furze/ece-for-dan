using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSavingAsDraft
{
    public class IfTheRequestIsMissingEntitlementMonths : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public IfTheRequestIsMissingEntitlementMonths(
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
            Api.Put($"{Url}/{Rs7.Id}", Command.SaveAsDraft(Rs7, rs7 => rs7.EntitlementMonths = null));
        }

        [Fact]
        public void ThenTheResponseShouldBeAHttp400()
        {
            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<UpdateRs7>(rs7 => rs7.EntitlementMonths)
                .WithMessage("'Entitlement Months' must not be empty.");
        }
    }
}