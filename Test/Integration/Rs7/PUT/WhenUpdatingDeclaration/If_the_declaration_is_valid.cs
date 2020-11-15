using Bard;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingDeclaration
{
    public class If_the_declaration_is_valid : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public If_the_declaration_is_valid(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        protected override void Arrange() =>
            Given
                .A_rs7_has_been_created()
                .The_rs7_has_been_submitted_for_peer_approval()
                .GetResult(storyData => Rs7Model = storyData.Rs7Model);

        protected override void Act()
        {
            UpdateRs7Declaration? updateRs7Declaration = ModelBuilder.UpdateRs7Declaration();

            When.Put($"{Url}/{Rs7Model.Id}/declaration", updateRs7Declaration);
        }

        [Fact]
        public void Raises_domain_event()
        {
            Rs7DeclarationUpdated? domainEvent = A_domain_event_should_be_fired<Rs7DeclarationUpdated>();
            domainEvent.Declaration?.Role.ShouldBe("role");
            domainEvent.Declaration?.ContactPhone.ShouldBe("123");
            domainEvent.Declaration?.FullName.ShouldBe("joe bloggs");
        }

        [Fact]
        public void Returns_success_response_code() =>
            Then.Response
                .ShouldBe
                .NoContent();

        [Fact]
        public void Updated_successfully() =>
            Then.Response
                .ShouldBe
                .NoContent();
    }
}