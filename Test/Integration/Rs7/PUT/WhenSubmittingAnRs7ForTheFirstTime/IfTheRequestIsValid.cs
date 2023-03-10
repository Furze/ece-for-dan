using System.Linq;
using Bard;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
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

            UpdateRs7Command =
                ModelBuilder.UpdateRs7(Rs7Model, rs7 => rs7.RollStatus = RollStatus.InternalReadyForReview);
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
            When.Put(Url + "/" + UpdateRs7Command.Id, UpdateRs7Command);
        }

        [Fact]
        public void ThenADomainEventShouldBePublishedRevisionData()
        {
            // Assert
            var domainEvent = A_domain_event_should_be_fired<Rs7Updated>();

            domainEvent.RevisionId.ShouldNotBe(0);
            domainEvent.RevisionNumber.ShouldBe(1);

            domainEvent.AdvanceMonths.ShouldNotBeNull();
            if (domainEvent.AdvanceMonths == null)
                return;

            domainEvent.AdvanceMonths.ElementAt(0).AllDay?.ShouldBeGreaterThan(0);
            domainEvent.AdvanceMonths.ElementAt(1).AllDay?.ShouldBeGreaterThan(0);
            domainEvent.AdvanceMonths.ElementAt(2).AllDay?.ShouldBeGreaterThan(0);
            domainEvent.AdvanceMonths.ElementAt(3).AllDay?.ShouldBeGreaterThan(0);

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
        public void ThenADomainEventShouldBePublishedWithRs7Data()
        {
            // Assert
            var domainEvent = A_domain_event_should_be_fired<Rs7Updated>();

            domainEvent.Id.ShouldNotBe(0);
            domainEvent.BusinessEntityId.ShouldNotBeNull();

            domainEvent.RollStatus.ShouldBe(RollStatus.InternalReadyForReview);
        }

        [Fact]
        public void ThenADomainEventShouldHaveSourceInternal()
        {
            // Assert
            var domainEvent = A_domain_event_should_be_fired<Rs7Updated>();

            domainEvent.Source.ShouldBe(Source.Internal);
        }

        [Fact]
        public void ThenAnIntegrationEventShouldBePublished()
        {
            // Assert
            var integrationEvent = An_integration_event_should_be_fired<Events.Integration.Protobuf.Ece.Rs7Updated>();

            integrationEvent.RollStatus.ShouldBe(Events.Integration.Protobuf.Ece.RollStatus.InternalReadyForReview);
            integrationEvent.RevisionNumber.ShouldBe(1);
            integrationEvent.RevisionDate.ShouldNotBeNull();
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