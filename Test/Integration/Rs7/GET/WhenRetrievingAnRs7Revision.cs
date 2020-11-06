using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.GET
{
    public class WhenRetrievingAnRs7Revision : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public WhenRetrievingAnRs7Revision(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState testState) : base(testSetUp, output, testState)
        {
        }

        protected override void Arrange()
        {
            If
                .A_rs7_has_been_created()
                .An_rs7_is_ready_for_internal_ministry_review()
                .UseResult(result => Rs7Updated = result);
        }

        protected override void Act()
        {
            Api.Get($"{Url}/{Rs7Updated.Id.ToString()}?revisionNumber=1");
        }

        [Fact]
        public void ThenTheResponseShouldBeOk()
        {
            Then
                .TheResponse
                .ShouldBe
                .Ok<Rs7Model>();
        }

        [Fact]
        public void ThenTheBusinessEntityIdShouldBePopulated()
        {
            Then
                .TheResponse
                .Content<Rs7Model>()
                .BusinessEntityId
                .ShouldNotBeNull();
        }
        
        [Fact]
        public void ThenTheOrganisationIdShouldBePopulated()
        {
            Then
                .TheResponse
                .Content<Rs7Model>()
                .OrganisationId
                .ShouldNotBeNull();
        }

        [Fact]
        public void ThenTheRevisionShouldBePopulated()
        {
            var rs7AndRevision = Then.TheResponse
                .Content<Rs7Model>();

            rs7AndRevision.Id.ShouldBeGreaterThan(0); // Should be Rs7.Id, not Rs7Revision.Id

            rs7AndRevision.RevisionNumber.ShouldBe(1);
            rs7AndRevision.RevisionDate.ShouldNotBeNull();

            rs7AndRevision.AdvanceMonths.ShouldNotBeNull();
            rs7AndRevision.AdvanceMonths.Count().ShouldBe(4);

            rs7AndRevision.EntitlementMonths.ShouldNotBeNull();
            rs7AndRevision.EntitlementMonths.Count().ShouldBe(4);

            rs7AndRevision.IsAttested.ShouldNotBeNull();
        }

        private Rs7Updated Rs7Updated
        {
            get => TestData.Rs7Submitted;
            set => TestData.Rs7Submitted = value;
        }
    }
}