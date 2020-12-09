using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Read.Model;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rolls
{
    public class WhenRetrievingAListOfRollsForAnOrganisationThatDoesNotExist : SpeedyIntegrationTestBase
    {
        private const string SomeRandomOrgNumber = "349523";

        public WhenRetrievingAListOfRollsForAnOrganisationThatDoesNotExist(RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output,
            testState)
        {
        }

        protected override void Arrange() =>
            Given
                .A_rs7_skeleton_has_been_created(submission =>
                    submission.OrganisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId)
                .The_rs7_has_been_submitted_for_peer_approval();

        protected override void Act() => When.Get($"api/rolls?organisation-number={SomeRandomOrgNumber}");

        [Fact]
        public void ThenTheResponseShouldBeOk() => Then.Response.ShouldBe.Ok();

        [Fact]
        public void ThenTheResponseShouldBeOfTheCorrectType() =>
            Then.Response.ShouldBe.Ok<CollectionModel<RollModel>>();

        [Fact]
        public void ThenTheResponseShouldBeContainZeroRolls()
        {
            var response = Then.Response.Content<CollectionModel<RollModel>>();

            response.Pagination.Count.ShouldBe(0);
            response.Data.Length.ShouldBe(0);
        }

        [Fact]
        public void Then_the_response_snapshot_should_be_ok() =>
            Then.Snapshot().Match<CollectionModel<RollModel>>();
    }
}