using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingEntitlementMonth
{
    public class IfTheExternallyCreatedMonthIsValid : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public IfTheExternallyCreatedMonthIsValid(
            RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output,
            TestState testState)
            : base(testSetUp, output, testState)
        {
        }

        protected override void Arrange()
        {
            //Externally Created, before Admin saves
            If
                .A_rs7_has_been_created( )
                .UseResult(result => Rs7Model = result);

            UpdateRs7EntitlementMonthCommand = Command.UpdateRs7EntitlementMonth(Rs7Model,
                1,
                em => em.Days.ElementAt(7).Under2 = 12);
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        private UpdateRs7EntitlementMonth UpdateRs7EntitlementMonthCommand
        {
            get => TestData.UpdateRs7EntitlementMonthCommand;
            set => TestData.UpdateRs7EntitlementMonthCommand = value;
        }


        protected override void Act()
        {
            Api.Put($"{Url}/{Rs7Model.Id}/entitlement-months/{UpdateRs7EntitlementMonthCommand.MonthNumber}",
                UpdateRs7EntitlementMonthCommand);
        }

        [Fact]
        public void ThenTheResponseShouldBeAHttp204()
        {
            Then.TheResponse
                .ShouldBe
                .NoContent();
        }

        [Fact]
        public void ThenADomainEventShouldBePublishedWithUpdatedData()
        {
            var domainEvent = Then
                .A_domain_event_should_be_fired<Domain.Event.Rs7EntitlementMonthUpdated>();

            domainEvent.EntitlementMonths.ElementAt(1)
                .Days.ElementAt(7).Under2.ShouldBe(12);
        }

        [Fact]
        public void ThenADomainEventShouldUpdateTheCurrentRevision()
        {
            // Assert
            var domainEvent = Then
                .A_domain_event_should_be_fired<Domain.Event.Rs7EntitlementMonthUpdated>();

            domainEvent.Source.ShouldBe(Source.Internal);
            domainEvent.RevisionNumber.ShouldBe(1);
        }

        [Fact]
        public void ThenAnIntegrationEventShouldBePublished()
        {
            // Assert
            var integrationEvent = Then
                .An_integration_event_should_be_fired<Rs7Updated>();

            integrationEvent.RevisionNumber.ShouldBe(1);
            integrationEvent.RevisionDate.ShouldNotBeNull();
            integrationEvent.EntitlementMonths.ElementAt(1)
                .Days.ElementAt(7).Under2.ShouldBe(12);
        }
    }
}