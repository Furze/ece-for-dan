using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.DELETE.WhenARs7IsRejected
{
    public class ItShouldBePossibleToDiscardTheRs7 : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";
        
        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }
        
        public ItShouldBePossibleToDiscardTheRs7(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState testState) : base(testSetUp, output, testState)
        {
        }

        protected override void Arrange()
        {
            If.A_rs7_has_been_created()
                .And().The()
                .Rs7_has_been_submitted_for_peer_approval()
                .And().Then().The()
                .Rs7_has_been_peer_review_rejected()
                .UseResult(rejected => Rs7Model = rejected);
        }

        protected override void Act()
        {
            // Act
            Api.Delete($"{Url}/{Rs7Model.Id}");
        }

        [Fact]
        public void ThenA204NoContentResponseShouldBeReturned()
        {
            Then.TheResponse.ShouldBe.NoContent();
        }
        
        [Fact]
        public void ThenADomainEventShouldBeFired()
        {
            Then
                .A_domain_event_should_be_fired<Rs7Discarded>();
        }
    }
}