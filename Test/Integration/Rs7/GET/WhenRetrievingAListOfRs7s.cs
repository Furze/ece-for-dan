using MoE.ECE.Domain.Read.Model;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.GET
{
    // ReSharper disable once InconsistentNaming
    public class WhenRetrievingAListOfRs7s : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public WhenRetrievingAListOfRs7s(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        protected override void Arrange() => Given.A_rs7_has_been_created();

        protected override void Act() => When.Get(Url);

        [Fact]
        public void ThenTheResponseShouldBeOk() =>
            Then
                .Response
                .ShouldBe
                .Ok<CollectionModel<Rs7Model>>();

        [Fact]
        public void ThenTheResponseShouldContainSomeItems()
        {
            CollectionModel<Rs7Model>? response = Then
                .Response
                .Content<CollectionModel<Rs7Model>>();

            response.Data.Length.ShouldBeGreaterThan(0);
            response.Pagination.Count.ShouldBeGreaterThan(0);
            response.Pagination.PageNumber.ShouldBe(1);
            response.Pagination.PageSize.ShouldBe(10);
        }

        [Fact]
        public void Then_the_response_snapshot_should_be_ok() =>
            Then.Snapshot().Match<CollectionModel<Rs7Model>>(IgnoreFieldsFor.CollectionModelRs7Model);
    }
}