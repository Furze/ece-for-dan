//using MoE.Rolls.Domain.Event;

using Bard;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.GET
{
    public class WhenRetrievingAnRs7ById : SpeedyIntegrationTestBase
    {
        protected WhenRetrievingAnRs7ById(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
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
            When.Get($"{Url}/{Rs7Model.Id.ToString()}");
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
        public void ThenTheDeclarationShouldBePopulated()
        {
            var rs7Model = Then.Response
                .Content<Rs7Model>();

            rs7Model.Declaration.ShouldNotBeNull();

            rs7Model.Declaration?.FullName.ShouldNotBeEmpty();
            rs7Model.Declaration?.ContactPhone.ShouldNotBeEmpty();
            rs7Model.Declaration?.Role.ShouldNotBeEmpty();
            rs7Model.Declaration?.IsDeclaredTrue.ShouldNotBeNull();
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

            rs7AndRevision.RevisionNumber.ShouldNotBeNull();
            rs7AndRevision.RevisionDate.ShouldNotBeNull();

            rs7AndRevision.AdvanceMonths.ShouldNotBeNull();
            rs7AndRevision.EntitlementMonths.ShouldNotBeNull();
            rs7AndRevision.IsAttested.ShouldNotBeNull();
        }
    }
}