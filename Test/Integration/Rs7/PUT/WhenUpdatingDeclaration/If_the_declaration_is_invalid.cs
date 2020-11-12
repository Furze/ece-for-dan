using Bard;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingDeclaration
{
    public class If_the_declaration_is_invalid : SpeedyIntegrationTestBase
    {
        public If_the_declaration_is_invalid(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created()
                .The_rs7_has_been_submitted_for_peer_approval()
                .GetResult(storyData => Rs7Model = storyData.Rs7Model);
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        [Fact]
        public void A_domain_event_is_not_fired()
        {
            var command = ModelBuilder.UpdateRs7Declaration(
                c => c.IsDeclaredTrue = false);

            When.Put($"{Url}/{Rs7Model.Id}/declaration", command);

            A_domain_event_should_not_be_fired<Rs7DeclarationUpdated>();
        }

        [Fact]
        public void Declaration_not_checked()
        {
            var command = ModelBuilder.UpdateRs7Declaration(
                c => c.IsDeclaredTrue = false);

            When.Put($"{Url}/{Rs7Model.Id}/declaration", command);

            Then.Response.ShouldBe.BadRequest.ForProperty("IsDeclaredTrue");
        }

        [Fact]
        public void Invalid_full_name()
        {
            var command = ModelBuilder.UpdateRs7Declaration(
                c => c.FullName = null);

            When.Put($"{Url}/{Rs7Model.Id}/declaration", command);

            Then.Response.ShouldBe.BadRequest.ForProperty("FullName");
        }

        [Fact]
        public void Invalid_phone()
        {
            var command = ModelBuilder.UpdateRs7Declaration(
                c => c.ContactPhone = "");

            When.Put($"{Url}/{Rs7Model.Id}/declaration", command);

            Then.Response.ShouldBe.BadRequest.ForProperty("ContactPhone");
        }

        [Fact]
        public void Invalid_role()
        {
            var command = ModelBuilder.UpdateRs7Declaration(
                c => c.Role = "");

            When.Put($"{Url}/{Rs7Model.Id}/declaration", command);

            Then.Response.ShouldBe.BadRequest.ForProperty<DeclarationModel>(x => x.Role);
        }
    }
}