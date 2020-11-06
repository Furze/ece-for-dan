using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingDeclaration
{
    public class If_the_declaration_is_invalid : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public If_the_declaration_is_invalid(
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
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        [Fact]
        public void Invalid_role()
        {
            var command = Command.UpdateRs7Declaration(
                c => c.Role = "");
            Api.Put($"{Url}/{this.Rs7Model.Id}/declaration", command);

            Then.TheResponse.ShouldBe.BadRequest.ForProperty<DeclarationModel>(x => x.Role);
        }

        [Fact]
        public void Invalid_phone()
        {
            var command = Command.UpdateRs7Declaration(
                c => c.ContactPhone = "");
            Api.Put($"{Url}/{this.Rs7Model.Id}/declaration", command);

            Then.TheResponse.ShouldBe.BadRequest.ForProperty("ContactPhone");
        }

        [Fact]
        public void A_domain_event_is_not_fired()
        {
            var command = Command.UpdateRs7Declaration(
                c => c.IsDeclaredTrue = false);
            Api.Put($"{Url}/{this.Rs7Model.Id}/declaration", command);

            Then.A_domain_event_should_not_be_fired<Rs7DeclarationUpdated>();
        }

        [Fact]
        public void Declaration_not_checked()
        {
            var command = Command.UpdateRs7Declaration(
                c => c.IsDeclaredTrue = false);
            Api.Put($"{Url}/{this.Rs7Model.Id}/declaration", command);

            Then.TheResponse.ShouldBe.BadRequest.ForProperty("IsDeclaredTrue");
        }

        [Fact]
        public void Invalid_full_name()
        {
            var command = Command.UpdateRs7Declaration(
                c => c.FullName = null);
            Api.Put($"{Url}/{this.Rs7Model.Id}/declaration", command);

            Then.TheResponse.ShouldBe.BadRequest.ForProperty("FullName");
        }
    }
}