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

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenSubmittingForApproval
{
    public class IfTheRequestIsValid : SpeedyIntegrationTestBase
    {
        public IfTheRequestIsValid(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

        protected override void Arrange()
        {
            Given
                .A_rs7_skeleton_has_been_created()
                .GetResult(result => Rs7Model = result.Rs7Model);
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        protected override void Act()
        {
            var command = ModelBuilder.SubmitRs7ForApproval(Rs7Model);

            // Act
            When.Post($"{Url}/{Rs7Model.Id}/submissions-for-approval", command);
        }

        [Fact]
        public void ThenADomainEventShouldBePublishedWithRs7Data()
        {
            // Assert
            var domainEvent = A_domain_event_should_be_fired<Rs7SubmittedForApproval>();

            domainEvent.Id.ShouldNotBe(0);
            domainEvent.BusinessEntityId.ShouldNotBeNull();

            domainEvent.RollStatus.ShouldBe(RollStatus.ExternalSubmittedForApproval);
        }

        [Fact]
        public void ThenADomainEventShouldNotUpdateTheCurrentRevision()
        {
            // Assert
            var domainEvent = A_domain_event_should_be_fired<Rs7SubmittedForApproval>();

            domainEvent.RevisionNumber.ShouldBe(1);
            domainEvent.RevisionId.ShouldNotBe(0);
        }

        [Fact]
        public void ThenADomainEventShouldPublishAdvanceMonths()
        {
            // Assert
            var domainEvent = A_domain_event_should_be_fired<Rs7SubmittedForApproval>();

            domainEvent.AdvanceMonths?.ElementAt(0).AllDay.GetValueOrDefault().ShouldBeGreaterThan(0);
            domainEvent.AdvanceMonths?.ElementAt(1).AllDay.GetValueOrDefault().ShouldBeGreaterThan(0);
            domainEvent.AdvanceMonths?.ElementAt(2).AllDay.GetValueOrDefault().ShouldBeGreaterThan(0);
            domainEvent.AdvanceMonths?.ElementAt(3).AllDay.GetValueOrDefault().ShouldBeGreaterThan(0);

            domainEvent.AdvanceMonths?.ElementAt(0).Sessional.GetValueOrDefault().ShouldBe(0);
            domainEvent.AdvanceMonths?.ElementAt(0).ParentLed.GetValueOrDefault().ShouldBe(0);
            domainEvent.AdvanceMonths?.ElementAt(1).Sessional.GetValueOrDefault().ShouldBe(0);
            domainEvent.AdvanceMonths?.ElementAt(1).ParentLed.GetValueOrDefault().ShouldBe(0);
            domainEvent.AdvanceMonths?.ElementAt(2).Sessional.GetValueOrDefault().ShouldBe(0);
            domainEvent.AdvanceMonths?.ElementAt(2).ParentLed.GetValueOrDefault().ShouldBe(0);
            domainEvent.AdvanceMonths?.ElementAt(3).Sessional.GetValueOrDefault().ShouldBe(0);
            domainEvent.AdvanceMonths?.ElementAt(3).ParentLed.GetValueOrDefault().ShouldBe(0);
        }

        [Fact]
        public void ThenAnIntegrationEventShouldBePublished()
        {
            // Assert
            var integrationEvent = An_integration_event_should_be_fired<Events.Integration.Protobuf.Ece.Rs7Updated>();

            integrationEvent.RollStatus.ShouldBe(Events.Integration.Protobuf.Ece.RollStatus
                .ExternalSubmittedForApproval);
            integrationEvent.RevisionNumber.ShouldNotBe(0);
            integrationEvent.RevisionDate.ShouldNotBeNull();
        }

        [Fact]
        public void ThenDomainEventShouldHaveSourceInternal()
        {
            var domainEvent = A_domain_event_should_be_fired<Rs7SubmittedForApproval>();

            domainEvent.Source.ShouldBe(Source.Internal);
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