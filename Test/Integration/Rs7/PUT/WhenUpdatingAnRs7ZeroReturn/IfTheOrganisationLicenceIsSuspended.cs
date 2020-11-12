using Bard;
using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingAnRs7ZeroReturn
{
    public class IfTheOrganisationLicenceIsSuspended : SpeedyIntegrationTestBase
    {
        public IfTheOrganisationLicenceIsSuspended(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

        protected override void Arrange()
        {
            Given
                .A_rs7_zero_return_has_been_created(rs7 =>
                    rs7.OrganisationId = ReferenceData.EceServices.NurtureMe2.RefOrganisationId)
                .GetResult(result => Rs7Model = result.Rs7Model);

            UpdateRs7Command = ModelBuilder.UpdateRs7(Rs7Model);
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        private UpdateRs7 UpdateRs7Command
        {
            get => TestData.SubmitRs7Command;
            set => TestData.SubmitRs7Command = value;
        }

        protected override void Act()
        {
            // Act
            When.Put($"{Url}/{Rs7Model.Id}", UpdateRs7Command);
        }

        [Fact]
        public void ThenADomainEventShouldNotBePublished()
        {
            // Assert
            A_domain_event_should_not_be_fired<Rs7Updated>();
        }

        [Fact]
        public void ThenTheResponseShouldBeAHttpBadRequest()
        {
            Then.Response
                .ShouldBe.BadRequest.WithErrorCode(ErrorCode.EceServiceIneligibleBecauseLicenceStatus);
        }
    }
}