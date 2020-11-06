using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.GET
{
    // ReSharper disable once InconsistentNaming
    public class WhenRetrievingAListOfRs7s : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public WhenRetrievingAListOfRs7s(
            RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output,
            TestState testState)
            : base(testSetUp, output, testState)
        {
        }

        protected override void Arrange()
        {
            If.A_rs7_has_been_created();
        }

        protected override void Act()
        {
            Api.Get(Url);
        }

        [Fact]
        public void ThenTheResponseShouldBeOk()
        {
            Then
                .TheResponse
                .ShouldBe
                .Ok<CollectionModel<Rs7Model>>();
        }

        [Fact]
        public void ThenTheResponseShouldContainSomeItems()
        {
            var response = Then
                .TheResponse
                .Content<CollectionModel<Rs7Model>>();

            response.Data.Length.ShouldBeGreaterThan(0);
            response.Pagination.Count.ShouldBeGreaterThan(0);
            response.Pagination.PageNumber.ShouldBe(1);
            response.Pagination.PageSize.ShouldBe(10);
        }
    }
}