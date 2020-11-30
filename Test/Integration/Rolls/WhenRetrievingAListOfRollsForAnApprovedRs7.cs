using System.Linq;
using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Read.Model;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rolls
{
    public class WhenRetrievingAListOfRollsForAnApprovedRs7 : SpeedyIntegrationTestBase
    {
        private const string Rs7 = "Rs7";

        private readonly int? _organisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId;

        public WhenRetrievingAListOfRollsForAnApprovedRs7(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        protected override void Arrange() =>
            Given
                .A_rs7_has_been_created(submission =>
                    submission.OrganisationId = _organisationId)
                .An_rs7_is_ready_for_internal_ministry_review()
                .And_the_rs7_has_been_approved();

        protected override void Act() => When.Get($"api/rolls?organisation-id={_organisationId}");


        [Fact]
        public void ThenTheResponseShouldBeOfTheCorrectType() =>
            Then.Response.ShouldBe.Ok<CollectionModel<RollModel>>();

        [Fact]
        public void ThenTheResponseShouldBeOk() => Then.Response.ShouldBe.Ok();

        [Fact]
        public void ThenTheResponseShouldContainAnRs7Roll()
        {
            var response = Then.Response.Content<CollectionModel<RollModel>>();

            response.Data.ShouldContain(model => model.RollType == Rs7);
        }

        [Fact]
        public void ThenTheRs7ReceivedDateShouldNotBeNull()
        {
            var response = Then.Response.Content<CollectionModel<RollModel>>();

            response.Data.Last(model => model.RollType == Rs7).Received.ShouldNotBeNull();
        }

        [Fact]
        public void ThenTheRs7RollShouldHaveAPopulatedStatus()
        {
            var response = Then.Response.Content<CollectionModel<RollModel>>();

            response.Data.Last(model => model.RollType == Rs7).Status.ShouldBe(RollStatus.InternalApproved);
        }

        [Fact]
        public void Then_the_response_snapshot_should_be_ok() =>
            Then.Snapshot().Match<CollectionModel<RollModel>>(IgnoreFieldsFor.CollectionModelRollModel);
    }
}