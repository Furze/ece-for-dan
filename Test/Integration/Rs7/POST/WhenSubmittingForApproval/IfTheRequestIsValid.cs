using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;
using Rs7Updated = MoE.Rolls.Domain.Integration.Events.Rs7.Rs7Updated;

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenSubmittingForApproval
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
                .UseResult(result => Rs7Model = result);

            SubmitRs7ForApproval = Command.SubmitRs7ForApproval(Rs7Model);
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        private SubmitRs7ForApproval SubmitRs7ForApproval
        {
            get => TestData.SubmitRs7ForApproval;
            set => TestData.SubmitRs7ForApproval = value;
        }

        protected override void Act()
        {
            // Act
            Api.Post($"{Url}/{Rs7Model.Id}/submissions-for-approval", SubmitRs7ForApproval);
        }

        [Fact]
        public void ThenADomainEventShouldBePublishedWithRs7Data()
        {
            // Assert
            var domainEvent = Then
                .A_domain_event_should_be_fired<Domain.Event.Rs7SubmittedForApproval>();

            domainEvent.Id.ShouldNotBe(0);
            domainEvent.BusinessEntityId.ShouldNotBeNull();

            domainEvent.RollStatus.ShouldBe(RollStatus.ExternalSubmittedForApproval);
        }

        [Fact]
        public void ThenADomainEventShouldNotUpdateTheCurrentRevision()
        {
            // Assert
            var domainEvent = Then
                .A_domain_event_should_be_fired<Domain.Event.Rs7SubmittedForApproval>();

            domainEvent.RevisionNumber.ShouldBe(1);
            domainEvent.RevisionId.ShouldNotBe(0);
            domainEvent.RevisionDate.ShouldNotBeNull();
        }

        [Fact]
        public void ThenADomainEventShouldPublishAdvanceMonths()
        {
            // Assert
            var domainEvent = Then
                .A_domain_event_should_be_fired<Domain.Event.Rs7SubmittedForApproval>();

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
        public void ThenDomainEventShouldHaveSourceInternal()
        {
            var domainEvent = Then
                .A_domain_event_should_be_fired<Domain.Event.Rs7SubmittedForApproval>();

            domainEvent.Source.ShouldBe(Source.Internal);
        }

        [Fact]
        public void ThenAnIntegrationEventShouldBePublished()
        {
            // Assert
            var integrationEvent = Then
                .An_integration_event_should_be_fired<Rs7Updated>();

            integrationEvent.RollStatus.ShouldBe(RollStatus.ExternalSubmittedForApproval);
            integrationEvent.RevisionNumber.ShouldNotBe(0);
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