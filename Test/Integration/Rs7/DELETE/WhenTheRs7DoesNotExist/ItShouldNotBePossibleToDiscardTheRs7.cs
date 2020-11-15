using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.DELETE.WhenTheRs7DoesNotExist
{
    public class ItShouldNotBePossibleToDiscardTheRs7 : SpeedyIntegrationTestBase
    {
        public ItShouldNotBePossibleToDiscardTheRs7(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";
        private const int ThisRs7IdDoesNotExist = 23324;

        protected override void Act()
        {
            // Act
            When.Delete($"{Url}/{ThisRs7IdDoesNotExist}");
        }

        [Fact]
        public void ThenA404NotFoundResponseShouldBeReturned()
        {
            Then.Response.ShouldBe.NotFound();
        }
    }
}