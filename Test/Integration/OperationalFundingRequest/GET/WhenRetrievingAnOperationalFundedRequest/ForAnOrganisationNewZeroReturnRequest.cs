using System;
using System.Collections.Generic;
using System.Linq;
using Bard;
using Events.Integration;
using Events.Integration.Protobuf.Ece;
using MoE.ECE.Domain.Event.OperationalFunding;
using MoE.ECE.Domain.Read.Model.OperationalFunding;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.OperationalFundingRequest.GET.WhenRetrievingAnOperationalFundedRequest
{
    public class ForAnOrganisationNewZeroReturnRequest : IntegrationTestBase<ECEStoryBook, ECEStoryData>
    {
        public ForAnOrganisationNewZeroReturnRequest(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private Guid _businessEntityId;

        protected override void Arrange() =>
            Given
                .A_rs7_zero_return_has_been_created(setup => setup.FundingPeriodYear = 2020)
                .GetResult(data => _businessEntityId = data.Rs7Model.BusinessEntityId.GetValueOrDefault());

        protected override void Act() =>
            When.Get($"api/operational-funding-requests?business-entity-id={_businessEntityId}&revision-number=1");

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
            var result = Then.Response.Content<ICollection<OperationalFundingRequestModel>>();

            result.Count.ShouldBe(1);
        }

        [Fact]
        public void ThenTheResponseShouldContainMatchingAdvanceMonths()
        {
            var result = Then.Response.Content<ICollection<OperationalFundingRequestModel>>();

            result.ShouldNotBeNull();
            result.ElementAt(0).MatchingAdvanceMonths.ShouldNotBeNull();
            result.ElementAt(0).MatchingAdvanceMonths?.Count.ShouldBe(1);
            result.ElementAt(0).MatchingAdvanceMonths?.ElementAt(0).MonthNumber.ShouldBe(2);
        }

        [Fact]
        public void Then_the_response_snapshot_should_be_ok() =>
            Then.Snapshot()
                .Match<ICollection<OperationalFundingRequestModel>>(IgnoreFieldsFor
                    .CollectionOperationalFundingRequestModel);
    }
}