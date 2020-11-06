using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
{
    public class IfTheRequestRollStatusIsNew : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public IfTheRequestRollStatusIsNew(
            RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output,
            TestState testState)
            : base(testSetUp, output, testState)
        {
        }

        protected override void Arrange()
        {
            If
                .A_rs7_has_been_created()
                .UseResult(result => Rs7Model = result);

            UpdateRs7Command = Command.UpdateRs7(Rs7Model, rs7 => rs7.RollStatus = RollStatus.ExternalNew);
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
            Then.A_domain_event_should_not_be_fired<Domain.Event.Rs7Updated>();
        }

        [Fact]
        public void ThenTheResponseShouldBeABadRequest()
        {
            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode(ErrorCode.InvalidUpdateRs7StatusNew);
        }
    }
}