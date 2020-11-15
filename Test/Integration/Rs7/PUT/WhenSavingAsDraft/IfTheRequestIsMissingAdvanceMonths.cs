using Bard;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSavingAsDraft
{
    public class IfTheRequestIsMissingAdvanceMonths : SpeedyIntegrationTestBase
    {
        public IfTheRequestIsMissingAdvanceMonths(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

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
            When.Put($"{Url}/{Rs7.Id}", ModelBuilder.SaveAsDraft(Rs7, rs7 => rs7.AdvanceMonths = null));
        }

        [Fact]
        public void ThenTheResponseShouldBeAHttp400()
        {
            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<UpdateRs7>(rs7 => rs7.AdvanceMonths)
                .WithMessage("'Advance Months' must not be empty.");
        }
    }
}