using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;
using Rs7Updated = MoE.Rolls.Domain.Integration.Events.Rs7.Rs7Updated;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
{
    public class IfTheRequestIsValid : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public IfTheRequestIsValid(
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
                .UseResult(result => Rs7Created = result);

            UpdateRs7Command = Command.UpdateRs7(Rs7Created, rs7 => rs7.RollStatus = RollStatus.InternalReadyForReview);
        }

        private Rs7Created Rs7Created
        {
            get => TestData.Rs7Created;
            set => TestData.Rs7Created = value;
        }

        private UpdateRs7 UpdateRs7Command
        {
            get => TestData.SubmitRs7Command;
            set => TestData.SubmitRs7Command = value;
        }

        protected override void Act()
        {
            // Act
            Api.Put(Url + "/" + UpdateRs7Command.Id, UpdateRs7Command);
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
        public void ThenADomainEventShouldBePublishedRevisionData()
        {
            // Assert
            var domainEvent = Then
                .A_domain_event_should_be_fired<Domain.Event.Rs7Updated>();

            domainEvent.RevisionId.ShouldNotBe(0);
            domainEvent.RevisionNumber.ShouldBe(1);
            domainEvent.RevisionDate.ShouldNotBeNull();

            domainEvent.AdvanceMonths.ElementAt(0).AllDay.ShouldBeGreaterThan(0);
            domainEvent.AdvanceMonths.ElementAt(1).AllDay.ShouldBeGreaterThan(0);
            domainEvent.AdvanceMonths.ElementAt(2).AllDay.ShouldBeGreaterThan(0);
            domainEvent.AdvanceMonths.ElementAt(3).AllDay.ShouldBeGreaterThan(0);

            domainEvent.AdvanceMonths.ElementAt(0).Sessional.ShouldBeNull();
            domainEvent.AdvanceMonths.ElementAt(0).ParentLed.ShouldBeNull();
            domainEvent.AdvanceMonths.ElementAt(1).Sessional.ShouldBeNull();
            domainEvent.AdvanceMonths.ElementAt(1).ParentLed.ShouldBeNull();
            domainEvent.AdvanceMonths.ElementAt(2).Sessional.ShouldBeNull();
            domainEvent.AdvanceMonths.ElementAt(2).ParentLed.ShouldBeNull();
            domainEvent.AdvanceMonths.ElementAt(3).Sessional.ShouldBeNull();
            domainEvent.AdvanceMonths.ElementAt(3).ParentLed.ShouldBeNull();

            domainEvent.IsAttested.ShouldBe(true);
        }

        [Fact]
        public void ThenADomainEventShouldHaveSourceInternal()
        {
            // Assert
            var domainEvent = Then
                .A_domain_event_should_be_fired<Domain.Event.Rs7Updated>();

            domainEvent.Source.ShouldBe(Source.Internal);
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
            Then.TheResponse!
                .ShouldBe
                .NoContent();
        }
    }
}