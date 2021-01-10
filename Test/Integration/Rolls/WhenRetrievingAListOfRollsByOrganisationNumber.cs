using System.Linq;
using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Read.Model;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rolls
{
    public class WhenRetrievingAListOfRollsByOrganisationNumber : SpeedyIntegrationTestBase
    {
        private const string Rs7 = "Rs7";
        private readonly int? _refOrganisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId;

        public WhenRetrievingAListOfRollsByOrganisationNumber(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }


        protected override void Arrange() =>
            Given
                .A_rs7_skeleton_has_been_created(submission =>
                {
                    submission.OrganisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId;
                    submission.FundingPeriodYear = 2020;
                })
                .The_rs7_has_been_submitted_for_peer_approval();

        protected override void Act() => When.Get($"api/rolls?organisation-id={_refOrganisationId}");

        [Fact]
        public void ThenTheResponseShouldBeOk() => Then.Response.ShouldBe.Ok();

        [Fact]
        public void ThenTheResponseShouldBeOfTheCorrectType() =>
            Then.Response.ShouldBe.Ok<CollectionModel<RollModel>>();

        [Fact]
        public void Then_the_response_snapshot_should_be_ok() =>
            Then.Snapshot().Match<CollectionModel<RollModel>>(IgnoreFieldsFor.CollectionModelRollModel);

        [Fact]
        public void ThenTheResponseShouldContainAnRs7Roll()
        {
            var response = Then.Response.Content<CollectionModel<RollModel>>();

            response.Data.ShouldContain(model => model.RollType == Rs7);
        }

        [Fact]
        public void ThenTheRs7RollShouldHaveAPopulatedBusinessEntityId()
        {
            var response = Then.Response.Content<CollectionModel<RollModel>>();

            response.Data.First(model => model.RollType == Rs7).BusinessEntityId.ShouldNotBeNull();
        }

        [Fact]
        public void ThenTheRs7RollShouldHaveAPopulatedStatus()
        {
            var response = Then.Response.Content<CollectionModel<RollModel>>();

            response.Data.First(model => model.RollType == Rs7).Status.ShouldNotBeNull();
        }
    }
}