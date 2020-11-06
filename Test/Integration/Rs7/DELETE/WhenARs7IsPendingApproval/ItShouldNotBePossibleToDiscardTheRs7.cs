using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.DELETE.WhenARs7IsPendingApproval
{
    public class ItShouldNotBePossibleToDiscardTheRs7 : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";
        
        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }
        
        public ItShouldNotBePossibleToDiscardTheRs7(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState testState) : base(testSetUp, output, testState)
        {
        }

        protected override void Arrange()
        {
            If
                .A_rs7_has_been_created()
                .An_rs7_is_ready_for_internal_ministry_review()
                .UseResult(created => Rs7Model = created);
        }

        protected override void Act()
        {
            // Act
            Api.Delete($"{Url}/{Rs7Model.Id}");
        }

        [Fact]
        public void ThenA400NoContentResponseShouldBeReturned()
        {
            Then.TheResponse.ShouldBe.BadRequest
                .WithErrorCode(ErrorCode.InvalidRollStatusForDiscard);
        }
        
        [Fact]
        public void ThenADomainEventShouldNotBeFired()
        {
            Then
                .A_domain_event_should_not_be_fired<Rs7Discarded>();
        }
    }
}