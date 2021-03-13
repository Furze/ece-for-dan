using System;
using System.Collections.Generic;
using System.Linq;
using Bard;
using Events.Integration;
using Events.Integration.Protobuf.Ece;
using MoE.ECE.Domain.Event.OperationalFunding;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Read.Model.OperationalFunding;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.OperationalFundingRequest.GET.WhenRetrievingAnOperationalFundedRequest
{
    public class ForAnOrganisationNewRequest : SpeedyIntegrationTestBase
    {
        public ForAnOrganisationNewRequest(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private Guid _businessEntityId;

        protected override void Arrange() =>
            Given
                .A_rs7_skeleton_has_been_created(rs7 =>
                {
                    rs7.FundingPeriod = FundingPeriodMonth.March;
                    rs7.FundingPeriodYear = 2020;
                })
                .An_rs7_is_ready_for_internal_ministry_review()
                .GetResult(data => _businessEntityId = data.Rs7Model.BusinessEntityId.GetValueOrDefault());

        protected override void Act() =>
            When.Get(
                $"api/operational-funding-requests?business-entity-id={_businessEntityId}&revision-number={1}");

        [Fact]
        public void ThenADomainEventShouldBePublished() =>
            A_domain_event_should_be_fired<OperationalFundingRequestCreated>();

        [Fact]
        public void ThenAnIntegrationEventShouldBeCalculated()
        {
            var integrationEvent = An_integration_event_should_be_fired<EntitlementCalculated>();

            integrationEvent
                .BusinessEntityType
                .ShouldBe(Constants.BusinessEntityTypes.Rs7);
        }

        [Fact]
        public void ThenTheResponseShouldBeOk() => Then.Response.ShouldBe.Ok();

        [Fact]
        public void ThenTheResponseShouldContain1Item()
        {
            var result =
                Then.Response.Content<ICollection<OperationalFundingRequestModel>>();

            result.Count.ShouldBe(1);
        }

        [Fact]
        public void ThenTheResponseShouldContainMatchingAdvanceMonths()
        {
            var result =
                Then.Response.Content<ICollection<OperationalFundingRequestModel>>();

            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.ElementAt(0).MatchingAdvanceMonths.ShouldNotBeNull();
            result.ElementAt(0).MatchingAdvanceMonths?.Count.ShouldBe(4);
            result.ElementAt(0).MatchingAdvanceMonths?.ElementAt(0).MonthNumber.ShouldBe(10);
            result.ElementAt(0).EntitlementMonths.ShouldNotBeNull();
            result.ElementAt(0).EntitlementMonths.ShouldNotBeEmpty();
            result.ElementAt(0).EntitlementMonths?.ElementAt(0).FundingComponents.ShouldNotBeNull();
            result.ElementAt(0).EntitlementMonths?.ElementAt(0).FundingComponents.ShouldNotBeEmpty();
            result.ElementAt(0).EntitlementMonths?.ElementAt(0).FundingComponents?.ElementAt(0).FundingComponentType
                .ShouldBe(FundingComponent.UnderTwo);
        }

        [Fact]
        public void ThenTheResponseShouldContainTotalForAdvanceMonths()
        {
            var result =
                Then.Response.Content<ICollection<OperationalFundingRequestModel>>();

            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.ElementAt(0).MatchingAdvanceMonths.ShouldNotBeNull();

            result.ElementAt(0).AdvanceMonths?.ElementAt(0).TotalAdvance.ShouldBe(418.68m);
            result.ElementAt(0).EntitlementMonths.ShouldNotBeNull();
            result.ElementAt(0).EntitlementMonths?.ElementAt(0).FundingComponents.ShouldNotBeNull();
            result.ElementAt(0).EntitlementMonths?.ElementAt(0).FundingComponents?.ElementAt(0).FundingComponentType
                .ShouldBe(FundingComponent.UnderTwo);
        }
        
        [Fact]
        public void Then_the_response_snapshot_should_be_ok() =>
            Then.Snapshot().Match<ICollection<OperationalFundingRequestModel>>(IgnoreFieldsFor.CollectionOperationalFundingRequestModel);
    }
}