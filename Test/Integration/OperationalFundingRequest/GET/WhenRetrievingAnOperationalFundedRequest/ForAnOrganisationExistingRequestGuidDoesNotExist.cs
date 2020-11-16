using System.Collections.Generic;
using MoE.ECE.Domain.Read.Model.OperationalFunding;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.OperationalFundingRequest.GET.WhenRetrievingAnOperationalFundedRequest
{
    public class ForAnOrganisationExistingRequestGuidDoesNotExist : SpeedyIntegrationTestBase
    {
        public ForAnOrganisationExistingRequestGuidDoesNotExist(RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output,
            testState)
        {
        }

        private const string BusinessEntityId = "CEF384F5-E3DF-4454-92AE-BEE528C6E48A";

        protected override void Arrange()
        {
        }

        protected override void Act()
        {
            When.Get("api/operational-funding-requests?" +
                     $"&business-entity-id={BusinessEntityId}");
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
    }
}