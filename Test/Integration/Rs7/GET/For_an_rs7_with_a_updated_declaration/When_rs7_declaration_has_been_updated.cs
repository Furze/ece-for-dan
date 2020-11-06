using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.GET.For_an_rs7_with_a_updated_declaration
{
    public class When_rs7_declaration_has_been_updated : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public When_rs7_declaration_has_been_updated(
            RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output,
            TestState testState)
            : base(testSetUp, output, testState)
        {
        }

        private Rs7Updated ArrangeResult
        {
            get => TestData.ArrangeResult;
            set => TestData.ArrangeResult = value;
        }

        private Rs7Updated ActResult
        {
            get => TestData.ActResult;
            set => TestData.ActResult = value;
        }

        protected override void Arrange()
        {
            If.A_rs7_has_been_created()
                .Then().The().Rs7_has_been_submitted_for_peer_approval()
                .Then().The().Rs7_Has_declaration_updated()
                .UseResult(res => this.ArrangeResult = res);
        }

        protected override void Act()
        {
            Api.Get($"{Url}/{ArrangeResult.Id}");
        }

        [Fact]
        public void Declaration_has_been_updated()
        {
            var response = Then
                .TheResponse
                .Content<Rs7Model>();

            response.Declaration?.Role.ShouldBe("role");
            response.Declaration?.ContactPhone.ShouldBe("123");
            response.Declaration?.FullName.ShouldBe("joe bloggs");
        }
    }
}