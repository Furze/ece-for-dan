using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingDeclaration
{
    public class If_the_declaration_is_valid : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public If_the_declaration_is_valid(
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
                .And().The()
                .Rs7_has_been_submitted_for_peer_approval()
                .UseResult(result => Rs7Model = result);

            this.UpdateRs7Declaration = Command.UpdateRs7Declaration();
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        private UpdateRs7Declaration UpdateRs7Declaration
        {
            get => TestData.UpdateRs7Declaration;
            set => TestData.UpdateRs7Declaration = value;
        }

        [Fact]
        public void Updated_successfully()
        {
            Then.TheResponse
                .ShouldBe
                .NoContent();
        }

        protected override void Act()
        {
            Api.Put($"{Url}/{this.Rs7Model.Id}/declaration", UpdateRs7Declaration);
        }

        [Fact]
        public void Returns_success_response_code()
        {
            Then.TheResponse
                .ShouldBe
                .NoContent();
        }

        [Fact]
        public void Raises_domain_event()
        {
            var domainEvent = Then
                .A_domain_event_should_be_fired<Domain.Event.Rs7DeclarationUpdated>();
            domainEvent.Declaration?.Role.ShouldBe("role");
            domainEvent.Declaration?.ContactPhone.ShouldBe("123");
            domainEvent.Declaration?.FullName.ShouldBe("joe bloggs");
        }
    }
}