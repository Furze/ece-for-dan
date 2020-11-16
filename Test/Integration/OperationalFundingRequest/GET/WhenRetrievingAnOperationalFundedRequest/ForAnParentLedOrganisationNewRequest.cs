using System;
using System.Collections.Generic;
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
        public ForAnParentLedOrganisationNewRequest(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private Guid _businessEntityId;
        private int _revisionNumber;

        protected override void Arrange()
        {
            Given
                .An_ParentLed_rs7_application_has_been_received()
                .UseResult(created =>
                {
                    _businessEntityId = created.BusinessEntityId;
                    _revisionNumber = created.RevisionNumber;
                });

            And
                .An_ParentLed_rs7_application_has_been_received(updated =>
            {
                updated.BusinessEntityId = _businessEntityId;
                updated.RevisionNumber = _revisionNumber + 1;
            });
        }

        protected override void Act()
        {
            When.Get($"api/operational-funding-requests?business-entity-id={_businessEntityId}&revision-number={_revisionNumber}");
        }

        [Fact]
        public void ThenTheResponseShouldBeOk()
        {
            Then.Response.ShouldBe.Ok();
        }

        [Fact]
        public void ThenTheResponseShouldContain1Item()
        {
            var result = Then.Response.Content<ICollection<OperationalFundingRequestModel>>();
            
            result.Count.ShouldBe(1);
        }
    }
}