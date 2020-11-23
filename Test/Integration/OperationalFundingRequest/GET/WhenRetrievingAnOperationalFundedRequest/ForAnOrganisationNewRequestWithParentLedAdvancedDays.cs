using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ForAnOrganisationNewRequestWithParentLedAdvancedDays : IntegrationTestBase<ECEStoryBook, ECEStoryData>
    {
        private Guid _businessEntityId;

        public ForAnOrganisationNewRequestWithParentLedAdvancedDays(RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output,
            testState)
        {
        }

        protected override void Arrange() =>
            Given
                .A_rs7_has_been_created(rs7 =>
                {
                    rs7.OrganisationId = ReferenceData.EceServices.LeestonPlaycentre.RefOrganisationId;
                })
                .An_rs7_is_ready_for_internal_ministry_review(rs7 =>
                {
                    if (rs7.AdvanceMonths != null)
                    {
                        foreach (var advanceMonth in rs7.AdvanceMonths)
                        {
                            advanceMonth.Sessional = null;
                            advanceMonth.AllDay = null;
                            advanceMonth.ParentLed = 5;
                        }
                    }
                })
                .GetResult(data => _businessEntityId = data.Rs7Model.BusinessEntityId.GetValueOrDefault());

        protected override void Act() =>
            When.Get($"api/operational-funding-requests?business-entity-id={_businessEntityId}&revision-number=1");

        [Fact]
        public void ThenTheResponseShouldBeOk() => Then.Response.ShouldBe.Ok();

        [Fact]
        public void ThenTheResponseShouldContain1Item()
        {
            var result = Then.Response.Content<ICollection<OperationalFundingRequestModel>>();

            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
            result.ElementAt(0).AdvanceMonths.ShouldNotBeNull();
            result.ElementAt(0).AdvanceMonths?.ElementAt(0).ParentLedWorkingDays.ShouldNotBeNull();
            result.ElementAt(0).AdvanceMonths?.ElementAt(0).ParentLedWorkingDays.ShouldBe(5);
        }

        [Fact]
        public void Then_the_response_snapshot_should_be_ok() =>
            Then.Snapshot()
                .Match<ICollection<OperationalFundingRequestModel>>(IgnoreFieldsFor
                    .CollectionOperationalFundingRequestModel);
    }
}