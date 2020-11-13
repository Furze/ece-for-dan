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

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSavingAsDraft
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
                .A_rs7_has_been_created()
                .GetResult(result => Rs7Model = result.Rs7Model);

            SaveAsDraftCommand = ModelBuilder.SaveAsDraft(Rs7Model, rs7 => rs7.RollStatus = RollStatus.ExternalDraft);
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        private SaveAsDraft SaveAsDraftCommand
        {
            get => TestData.SaveAsDraftCommand;
            set => TestData.SaveAsDraftCommand = value;
        }

        protected override void Act()
        {
            // Act
            When.Put($"{Url}/{Rs7Model.Id}", SaveAsDraftCommand);
        }

        [Fact]
        public void ThenADomainEventShouldBePublishedWithRs7Data()
        {
            // Assert
            var domainEvent = A_domain_event_should_be_fired<Rs7Updated>();

            domainEvent.Id.ShouldNotBe(0);
            domainEvent.BusinessEntityId.ShouldNotBeNull();

            domainEvent.RollStatus.ShouldBe(RollStatus.ExternalDraft);
        }

        [Fact]
        public void ThenADomainEventShouldHaveSourceInternal()
        {
            // Assert
            var domainEvent = A_domain_event_should_be_fired<Rs7Updated>();

            domainEvent.Source.ShouldBe(Source.Internal);
        }

        [Fact]
        public void ThenADomainEventShouldPublishAdvanceMonths()
        {
            // Assert
            var domainEvent = A_domain_event_should_be_fired<Rs7Updated>();

            if (domainEvent.AdvanceMonths == null)
                return;

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
        public void ThenADomainEventShouldUpdateTheCurrentRevision()
        {
            // Assert
            var domainEvent = A_domain_event_should_be_fired<Rs7Updated>();

            domainEvent.RevisionId.ShouldNotBe(0);
            domainEvent.RevisionNumber.ShouldBe(1);
            domainEvent.RevisionDate.ShouldNotBeNull();
        }

        [Fact]
        public void ThenAnIntegrationEventShouldBePublished()
        {
            // Assert
            var integrationEvent = An_integration_event_should_be_fired<Events.Integration.Protobuf.Roll.Rs7Updated>();

            integrationEvent.RollStatus.ShouldBe(Events.Integration.Protobuf.Roll.RollStatus.ExternalDraft);
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