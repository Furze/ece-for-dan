using System;
using System.Collections.Generic;
using System.Linq;
using Bard;
using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Model.OperationalFunding;
using MoE.ECE.Domain.Read.Model.OperationalFunding;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.OperationalFundingRequest.GET.WhenRetrievingAnOperationalFundedRequest
{
    public class ForAnOrganisationWithWithheldFunding : IntegrationTestBase<ECEStoryBook, ECEStoryData>
    {
        private Guid _businessEntityId;

        public ForAnOrganisationWithWithheldFunding(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        protected override void Arrange() =>
            Given
                .A_rs7_skeleton_has_been_created(rs7 =>
                    rs7.OrganisationId = ReferenceData.EceServices.FoxtonPlaycentre.RefOrganisationId)
                .An_rs7_is_ready_for_internal_ministry_review(rs7 =>
                {
                    if (rs7.AdvanceMonths != null)
                    {
                        rs7.AdvanceMonths[0].AllDay = 0;
                        rs7.AdvanceMonths[1].AllDay = 0;
                        rs7.AdvanceMonths[2].AllDay = 0;
                        rs7.AdvanceMonths[3].AllDay = 0;
                    }
                })
                .GetResult(data => _businessEntityId = data.Rs7Model.BusinessEntityId.GetValueOrDefault());

        protected override void Act() =>
            When.Get(
                $"api/operational-funding-requests?business-entity-id={_businessEntityId}&revision=1");

        [Fact]
        public void ThenTheResponseShouldBeOk() => Then.Response.ShouldBe.Ok();
 
        [Fact]
        public void Then_the_business_exceptions_should_not_be_empty()
        {
            var response = Then.Response.Content<ICollection<OperationalFundingRequestModel>>();
                
            response.First().BusinessExceptions.ShouldNotBeEmpty();
            
            response.First().BusinessExceptions?.ShouldContain(model => model.Key == FundingWithheldBusinessException.FundingWithheldKey);
        }

        [Fact]
        public void Then_the_response_snapshot_should_be_ok() =>
            Then.Snapshot()
                .Match<ICollection<OperationalFundingRequestModel>>(IgnoreFieldsFor
                    .CollectionOperationalFundingRequestModel);
    }
}