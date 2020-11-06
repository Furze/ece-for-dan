using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingAnRs7ZeroReturn
{
    public class IfTheOrganisationLicenceIsSuspended : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public IfTheOrganisationLicenceIsSuspended(
            RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output,
            TestState testState)
            : base(testSetUp, output, testState)
        {
        }

        protected override void Arrange()
        {
            If
                .A_rs7_zero_return_has_been_created(rs7 => rs7.OrganisationId = ReferenceData.EceServices.NurtureMe2.RefOrganisationId)
                .UseResult(result => Rs7Model = result);

            UpdateRs7Command = Command.UpdateRs7(Rs7Model);
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
            Api.Put($"{Url}/{Rs7Model.Id}", UpdateRs7Command);
        }

        [Fact]
        public void ThenADomainEventShouldNotBePublished()
        {
            // Assert
            Then.A_domain_event_should_not_be_fired<Domain.Event.Rs7Updated>();
        }

        [Fact]
        public void ThenTheResponseShouldBeAHttpBadRequest()
        {
            Then.TheResponse
                .ShouldBe.BadRequest.WithErrorCode(ErrorCode.EceServiceIneligibleBecauseLicenceStatus);
        }
    }
}