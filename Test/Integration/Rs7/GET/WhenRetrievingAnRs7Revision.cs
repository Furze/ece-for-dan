using System.Linq;
using Bard;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.GET
{
    public class WhenRetrievingAnRs7Revision : SpeedyIntegrationTestBase
    {
        public WhenRetrievingAnRs7Revision(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created()
                .An_rs7_is_ready_for_internal_ministry_review()
                .GetResult(storyData => Rs7Model = storyData.Rs7Model);
        }

        protected override void Act()
        {
            When.Get($"{Url}/{Rs7Model.Id.ToString()}?revisionNumber=1");
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        [Fact]
        public void ThenTheBusinessEntityIdShouldBePopulated()
        {
            Then
                .Response
                .Content<Rs7Model>()
                .BusinessEntityId
                .ShouldNotBeNull();
        }

        [Fact]
        public void ThenTheOrganisationIdShouldBePopulated()
        {
            Then
                .Response
                .Content<Rs7Model>()
                .OrganisationId
                .ShouldNotBeNull();
        }

        [Fact]
        public void ThenTheResponseShouldBeOk()
        {
            Then
                .Response
                .ShouldBe
                .Ok<Rs7Model>();
        }

        [Fact]
        public void ThenTheRevisionShouldBePopulated()
        {
            var rs7AndRevision = Then.Response
                .Content<Rs7Model>();

            rs7AndRevision.Id.ShouldBeGreaterThan(0); // Should be Rs7.Id, not Rs7Revision.Id

            rs7AndRevision.RevisionNumber.ShouldBe(1);
            rs7AndRevision.RevisionDate.ShouldNotBeNull();

            rs7AndRevision.AdvanceMonths.ShouldNotBeNull();
            rs7AndRevision.AdvanceMonths?.Count().ShouldBe(4);

            rs7AndRevision.EntitlementMonths.ShouldNotBeNull();
            rs7AndRevision.EntitlementMonths?.Count().ShouldBe(4);

            rs7AndRevision.IsAttested.ShouldNotBeNull();
        }
    }
}