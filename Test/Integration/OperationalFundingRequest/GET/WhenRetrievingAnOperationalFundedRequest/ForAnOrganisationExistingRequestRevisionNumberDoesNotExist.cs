using System.Collections.Generic;
using MoE.ECE.Domain.Read.Model.OperationalFunding;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.OperationalFundingRequest.GET.WhenRetrievingAnOperationalFundedRequest
{
    public class ForAnOrganisationExistingRequestRevisionNumberDoesNotExist : SpeedyIntegrationTestBase
    {
        public ForAnOrganisationExistingRequestRevisionNumberDoesNotExist(RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output,
            testState)
        {
        }

        private const string BusinessEntityId = "C8EE3A99-1994-44E4-94E1-AB222589F1AC";
        private const string RevisionNumber = "100";

        protected override void Arrange()
        {
        }

        protected override void Act()
        {
            When.Get("api/operational-funding-requests?" +
                     $"&business-entity-id={BusinessEntityId}" +
                     $"&revision-number={RevisionNumber}");
        }

        [Fact]
        public void ThenTheResponseShouldBeOk()
        {
            Then.Response.ShouldBe.Ok();
        }

        [Fact]
        public void ThenTheResponseShouldContainItems()
        {
            var result = Then.Response.Content<ICollection<OperationalFundingRequestModel>>();

            result.Count.ShouldBe(0);
        }
        
        [Fact]
        public void Then_the_response_snapshot_should_be_ok()
        {
            Then.Snapshot().Match<ICollection<OperationalFundingRequestModel>>();
        }
    }
}