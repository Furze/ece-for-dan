using Bard;
using Events.Integration.Protobuf.Entitlement;
using MoE.ECE.CLI.Data;
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
    public class ForAnOrganisationWithWithheldFunding : SpeedyIntegrationTestBase
    {
        public ForAnOrganisationWithWithheldFunding(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

        protected override void Arrange()
        {
            Given
                .A_rs7_skeleton_has_been_created(rs7 =>
                    rs7.OrganisationId = ReferenceData.EceServices.FoxtonPlaycentre.RefOrganisationId)
                .GetResult(result => Rs7Model = result.Rs7Model);

            UpdateRs7Command =
                ModelBuilder.UpdateRs7(Rs7Model, rs7 =>
                {
                    rs7.RollStatus = RollStatus.InternalReadyForReview;

                    if (rs7.AdvanceMonths == null)
                    {
                        return;
                    }

                    rs7.AdvanceMonths[0].AllDay = 0;
                    rs7.AdvanceMonths[1].AllDay = 0;
                    rs7.AdvanceMonths[2].AllDay = 0;
                    rs7.AdvanceMonths[3].AllDay = 0;
                });
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

        protected override void Act() =>
            // Act
            When.Put(Url + "/" + UpdateRs7Command.Id, UpdateRs7Command);

        [Fact]
        public void Then_an_entitlementCalculated_integration_event_should_be_published()
        {
            // Assert
            var integrationEvent = An_integration_event_should_be_fired<EntitlementCalculated>();

            integrationEvent.Exceptions.ShouldNotBeEmpty();
        }

        [Fact]
        public void ThenADomainEventShouldBePublishedRevisionData()
        {
            // Assert
            var domainEvent = A_domain_event_should_be_fired<Rs7Updated>();

            domainEvent.ShouldSatisfyAllConditions(() => domainEvent.RevisionId.ShouldNotBe(0),
                () => domainEvent.RevisionNumber.ShouldBe(1));
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
            var integrationEvent = An_integration_event_should_be_fired<Events.Integration.Protobuf.Roll.Rs7Updated>();

            integrationEvent.ShouldSatisfyAllConditions(
                () => integrationEvent.RollStatus.ShouldBe(Events.Integration.Protobuf.Roll.RollStatus
                    .InternalReadyForReview),
                () => integrationEvent.RevisionNumber.ShouldBe(1),
                () => integrationEvent.RevisionDate.ShouldNotBeNull()
            );
        }

        [Fact]
        public void ThenTheResponseShouldBeAHttp204() =>
            Then.Response
                .ShouldBe
                .NoContent();
    }
}