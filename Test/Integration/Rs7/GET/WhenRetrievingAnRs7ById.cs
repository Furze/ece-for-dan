//using MoE.Rolls.Domain.Event;

using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.GET
{
    public class WhenRetrievingAnRs7ById : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public WhenRetrievingAnRs7ById(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState testState) : base(testSetUp, output, testState)
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
            Api.Get($"{Url}/{Rs7Updated.Id.ToString()}");
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

            rs7AndRevision.RevisionNumber.ShouldNotBeNull();
            rs7AndRevision.RevisionDate.ShouldNotBeNull();

            rs7AndRevision.AdvanceMonths.ShouldNotBeNull();
            rs7AndRevision.EntitlementMonths.ShouldNotBeNull();
            rs7AndRevision.IsAttested.ShouldNotBeNull();
        }

        [Fact]
        public void ThenTheDeclarationShouldBePopulated()
        {
            var rs7Model = Then.TheResponse
                .Content<Rs7Model>();

            rs7Model.Declaration.ShouldNotBeNull();

            rs7Model.Declaration?.FullName.ShouldNotBeEmpty();
            rs7Model.Declaration?.ContactPhone.ShouldNotBeEmpty();
            rs7Model.Declaration?.Role.ShouldNotBeEmpty();
            rs7Model.Declaration?.IsDeclaredTrue.ShouldNotBeNull();
        }

        private Rs7Updated Rs7Updated
        {
            get => TestData.Rs7Submitted;
            set => TestData.Rs7Submitted = value;
        }
    }
}