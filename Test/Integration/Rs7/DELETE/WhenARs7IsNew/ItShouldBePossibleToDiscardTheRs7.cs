using Bard;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.DELETE.WhenARs7IsNew
{
    public class ItShouldBePossibleToDiscardTheRs7 : SpeedyIntegrationTestBase
    {
        protected ItShouldBePossibleToDiscardTheRs7(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created()
                .GetResult(storyData => Rs7Model = storyData.Rs7Model);
        }

        protected override void Act()
        {
            // Act
            When.Delete($"{Url}/{Rs7Model.Id}");
        }

        [Fact]
        public void ThenA204NoContentResponseShouldBeReturned()
        {
            Then.Response.ShouldBe.NoContent();
        }

        [Fact]
        public void ThenADomainEventShouldBeFired()
        {
            A_domain_event_should_be_fired<Rs7Discarded>();
        }
    }
}