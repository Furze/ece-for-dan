using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.DELETE.WhenTheRs7DoesNotExist
{
    public class ItShouldNotBePossibleToDiscardTheRs7 : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";
        private const int ThisRs7IdDoesNotExist = 23324;
        
        public ItShouldNotBePossibleToDiscardTheRs7(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState testState) : base(testSetUp, output, testState)
        {
        }

        protected override void Act()
        {
            // Act
            Api.Delete($"{Url}/{ThisRs7IdDoesNotExist}");
        }

        [Fact]
        public void ThenA404NotFoundResponseShouldBeReturned()
        {
            Then.TheResponse.ShouldBe.NotFound();
        }
    }
}