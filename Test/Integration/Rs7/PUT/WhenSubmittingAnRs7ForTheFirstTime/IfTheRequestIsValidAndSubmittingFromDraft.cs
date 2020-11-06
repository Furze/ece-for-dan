using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;
using Rs7Updated = MoE.Rolls.Domain.Integration.Events.Rs7.Rs7Updated;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
{
    public class IfTheRequestIsValidAndSubmittingFromDraft : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public IfTheRequestIsValidAndSubmittingFromDraft(
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
                .And_the_rs7_has_been_saved_as_draft()
                .UseResult(result => Rs7Model = result);

            UpdateRs7Command = Command.UpdateRs7(Rs7Model, rs7 => rs7.RollStatus = RollStatus.InternalReadyForReview);
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        private UpdateRs7 UpdateRs7Command
        {
            get => TestData.SubmitRs7Command;
            set => TestData.SubmitRs7Command = value;
        }

        protected override void Act()
        {
            // Act
            Api.Put($"{Url}/{Rs7Model.Id}", UpdateRs7Command);
        }

        [Fact]
        public void ThenADomainEventShouldBePublishedWithRs7Data()
        {
            // Assert
            var domainEvent = Then
                .A_domain_event_should_be_fired<Domain.Event.Rs7Updated>();

            domainEvent.Id.ShouldNotBe(0);
            domainEvent.BusinessEntityId.ShouldNotBeNull();

            domainEvent.RollStatus.ShouldBe(RollStatus.InternalReadyForReview);
        }

        [Fact]
        public void ThenADomainEventShouldUpdateTheCurrentRevision()
        {
            // Assert
            var domainEvent = Then
                .A_domain_event_should_be_fired<Domain.Event.Rs7Updated>();

            domainEvent.RevisionId.ShouldNotBe(0);
            domainEvent.RevisionNumber.ShouldBe(1);
            domainEvent.RevisionDate.ShouldNotBeNull();

            domainEvent.AdvanceMonths.Count().ShouldBe(4);

            domainEvent.EntitlementMonths.Count().ShouldBe(4);

            domainEvent.IsAttested.ShouldBe(true);
        }

        [Fact]
        public void ThenADomainEventShouldPublishAdvanceMonths()
        {
            // Assert
            var domainEvent = Then
                .A_domain_event_should_be_fired<Domain.Event.Rs7Updated>();

            domainEvent.AdvanceMonths.ElementAt(0).AllDay.GetValueOrDefault().ShouldBeGreaterThan(0);
            domainEvent.AdvanceMonths.ElementAt(1).AllDay.GetValueOrDefault().ShouldBeGreaterThan(0);
            domainEvent.AdvanceMonths.ElementAt(2).AllDay.GetValueOrDefault().ShouldBeGreaterThan(0);
            domainEvent.AdvanceMonths.ElementAt(3).AllDay.GetValueOrDefault().ShouldBeGreaterThan(0);
            
            domainEvent.AdvanceMonths.ElementAt(0).Sessional.GetValueOrDefault().ShouldBe(0);
            domainEvent.AdvanceMonths.ElementAt(0).ParentLed.GetValueOrDefault().ShouldBe(0);
            domainEvent.AdvanceMonths.ElementAt(1).Sessional.GetValueOrDefault().ShouldBe(0);
            domainEvent.AdvanceMonths.ElementAt(1).ParentLed.GetValueOrDefault().ShouldBe(0);
            domainEvent.AdvanceMonths.ElementAt(2).Sessional.GetValueOrDefault().ShouldBe(0);
            domainEvent.AdvanceMonths.ElementAt(2).ParentLed.GetValueOrDefault().ShouldBe(0);
            domainEvent.AdvanceMonths.ElementAt(3).Sessional.GetValueOrDefault().ShouldBe(0);
            domainEvent.AdvanceMonths.ElementAt(3).ParentLed.GetValueOrDefault().ShouldBe(0);
        }

        [Fact]
        public void ThenAnIntegrationEventShouldBePublished()
        {
            // Assert
            var integrationEvent = Then
                .An_integration_event_should_be_fired<Rs7Updated>();

            integrationEvent.RollStatus.ShouldBe(RollStatus.InternalReadyForReview);
            integrationEvent.RevisionNumber.ShouldBe(1);
            integrationEvent.RevisionDate.ShouldNotBeNull();
        }

        [Fact]
        public void ThenTheResponseShouldBeAHttp204()
        {
            Then.TheResponse
                .ShouldBe
                .NoContent();
        }
    }
}