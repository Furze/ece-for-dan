using Bard;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.GET.For_an_rs7_with_a_updated_declaration
{
    public class When_rs7_declaration_has_been_updated : SpeedyIntegrationTestBase
    {
        public When_rs7_declaration_has_been_updated(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created()
                .The_rs7_has_been_submitted_for_peer_approval()
                .The_rs7_declaration_has_been_updated()
                .GetResult(storyData => Rs7Model = storyData.Rs7Model);
        }

        protected override void Act()
        {
            When.Get($"{Url}/{Rs7Model.Id}");
        }

        [Fact]
        public void Declaration_has_been_updated()
        {
            var response = Then
                .Response
                .Content<Rs7Model>();

            response.Declaration?.Role.ShouldBe("role");
            response.Declaration?.ContactPhone.ShouldBe("123");
            response.Declaration?.FullName.ShouldBe("joe bloggs");
        }
    }
}