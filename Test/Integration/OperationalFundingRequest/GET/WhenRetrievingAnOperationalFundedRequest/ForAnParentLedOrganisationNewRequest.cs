using System;
using System.Collections.Generic;
using Bard;
using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Read.Model.OperationalFunding;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.OperationalFundingRequest.GET.WhenRetrievingAnOperationalFundedRequest
{
    public class ForAnParentLedOrganisationNewRequest : IntegrationTestBase<ECEStoryBook, ECEStoryData>
    {
        public ForAnParentLedOrganisationNewRequest(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private Guid _businessEntityId;

        protected override void Arrange() =>
            Given
                .A_rs7_skeleton_has_been_created(rs7 =>
                {
                    rs7.OrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId;
                    rs7.FundingPeriodYear = 2020;
                })
                .An_rs7_is_ready_for_internal_ministry_review()
                .GetResult(data => _businessEntityId = data.Rs7Model.BusinessEntityId.GetValueOrDefault());

        protected override void Act() =>
            When.Get(
                $"api/operational-funding-requests?business-entity-id={_businessEntityId}");

        [Fact]
        public void ThenTheResponseShouldBeOk() => Then.Response.ShouldBe.Ok();

        [Fact]
        public void ThenTheResponseShouldContain1Item()
        {
            var result = Then.Response.Content<ICollection<OperationalFundingRequestModel>>();

            result.Count.ShouldBe(1);
        }
        
        [Fact]
        public void Then_the_response_snapshot_should_be_ok() =>
            Then.Snapshot().Match<ICollection<OperationalFundingRequestModel>>(IgnoreFieldsFor.CollectionOperationalFundingRequestModel);
    }
}