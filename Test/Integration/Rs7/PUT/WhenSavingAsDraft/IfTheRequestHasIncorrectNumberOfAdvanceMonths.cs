using System.Linq;
using Bard;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSavingAsDraft
{
    public class IfTheRequestHasIncorrectNumberOfAdvanceMonths : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public IfTheRequestHasIncorrectNumberOfAdvanceMonths(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private Rs7Model Rs7
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        protected override void Arrange() =>
            Given
                .A_rs7_has_been_created()
                .GetResult(created => Rs7 = created.Rs7Model);

        protected override void Act() =>
            // Act
            When.Put($"{Url}/{Rs7.Id}", ModelBuilder.SaveAsDraft(Rs7, rs7 =>
            {
                rs7.AdvanceMonths = Enumerable.Range(6, 3)
                    .Select(month => new Rs7AdvanceMonthModel {MonthNumber = month}).ToArray();
            }));

        [Fact]
        public void ThenTheResponseShouldBeAHttp400() =>
            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7Model>(rs7 => rs7.AdvanceMonths)
                .WithMessage("A total of 4 months must be provided.");
    }
}