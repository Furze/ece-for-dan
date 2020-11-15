using Bard;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSavingAsDraft
{
    public class IfTheRequestIsForUnknownRs7 : SpeedyIntegrationTestBase
    {
        public IfTheRequestIsForUnknownRs7(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";
        private const int SomeRandomRs7Id = 53232;

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created()
                .GetResult(created => Rs7 = created.Rs7Model);
        }

        private Rs7Model Rs7
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        protected override void Act()
        {
            // Act
            When.Put($"{Url}/{SomeRandomRs7Id}", ModelBuilder.SaveAsDraft(Rs7));
        }

        [Fact]
        public void ThenTheResponseShouldBeAHttp404()
        {
            Then.Response
                .ShouldBe
                .NotFound();
        }
    }
}