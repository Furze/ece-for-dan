using System.Linq;
using Bard;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using Rs7Updated = Events.Integration.Protobuf.Roll.Rs7Updated;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingEntitlementMonth
{
    public class IfTheExternallyDraftedMonthIsValid : SpeedyIntegrationTestBase
    {
        protected IfTheExternallyDraftedMonthIsValid(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

        protected override void Arrange()
        {
            //Externally Created, before Admin saves
            Given
                .A_rs7_has_been_created()
                .And_the_rs7_has_been_saved_as_draft()
                .GetResult(storyData => Rs7Model = storyData.Rs7Model);
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        protected override void Act()
        {
            var update = ModelBuilder.UpdateRs7EntitlementMonth(Rs7Model,
                1,
                em =>
                {
                    if(em.Days != null)
                        em.Days.ElementAt(7).Under2 = 12;
                });

            When.Put($"{Url}/{Rs7Model.Id}/entitlement-months/{update.MonthNumber}",
                update);
        }

        [Fact]
        public void ThenADomainEventShouldBePublishedWithUpdatedData()
        {
            var domainEvent = A_domain_event_should_be_fired<Rs7EntitlementMonthUpdated>();

            domainEvent.EntitlementMonths.ShouldNotBeNull();
            domainEvent.EntitlementMonths?.ElementAt(1).Days.ShouldNotBeNull();
            domainEvent.EntitlementMonths?.ElementAt(1)
                .Days?.ElementAt(7).Under2.ShouldBe(12);
        }

        [Fact]
        public void ThenADomainEventShouldUpdateTheCurrentRevision()
        {
            // Assert
            var domainEvent = A_domain_event_should_be_fired<Rs7EntitlementMonthUpdated>();

            domainEvent.Source.ShouldBe(Source.Internal);
            domainEvent.RevisionNumber.ShouldBe(1);
        }

        [Fact]
        public void ThenAnIntegrationEventShouldBePublished()
        {
            // Assert
            var integrationEvent = An_integration_event_should_be_fired<Rs7Updated>();

            integrationEvent.RevisionNumber.ShouldBe(1);
            integrationEvent.RevisionDate.ShouldNotBeNull();
            
            //TODO: MISSING ELEMENT
            // integrationEvent.EntitlementMonths.ElementAt(1)
            //     .Days.ElementAt(7).Under2.ShouldBe(12);
        }

        [Fact]
        public void ThenTheResponseShouldBeAHttp204()
        {
            Then.Response
                .ShouldBe
                .NoContent();
        }
    }
}