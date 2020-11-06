using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingEntitlementMonth
{
    public class IfTheExternallySubmittedMonthIsValid : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public IfTheExternallySubmittedMonthIsValid(
            RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output,
            TestState testState)
            : base(testSetUp, output, testState)
        {
        }

        protected override void Arrange()
        {
            //Externally Created, Submitted, Approved. Internal user updates
            If
                .A_rs7_has_been_created( )
                .And().The()
                .An_rs7_is_ready_for_internal_ministry_review(updateRs7 =>
                {
                    updateRs7.Declaration = new DeclarationModel()
                    {
                        FullName = "Declarer",
                        Role = "Boss",
                        ContactPhone = "123",
                        IsDeclaredTrue = true
                    };
                })
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

            domainEvent.Declaration?.FullName.ShouldBe("Declarer");
            
            domainEvent.EntitlementMonths?.ElementAt(1)
                .Days?.ElementAt(7).Under2.ShouldBe(12);
        }

        [Fact]
        public void ThenADomainEventShouldUpdateTheCurrentRevision()
        {
            // Assert
            var domainEvent = Then
                .A_domain_event_should_be_fired<Domain.Event.Rs7EntitlementMonthUpdated>();

            domainEvent.Source.ShouldBe(Source.Internal);
            domainEvent.RevisionNumber.ShouldBe(2);
            domainEvent.RevisionDate.ShouldNotBeNull();
        }

        [Fact]
        public void ThenAnIntegrationEventShouldBePublished()
        {
            // Assert
            var integrationEvent = Then
                .An_integration_event_should_be_fired<Rs7Updated>();

            integrationEvent.RevisionNumber.ShouldBe(2);
            integrationEvent.RevisionDate.ShouldNotBeNull();
            integrationEvent.EntitlementMonths.ElementAt(1)
                .Days.ElementAt(7).Under2.ShouldBe(12);
        }
    }
}