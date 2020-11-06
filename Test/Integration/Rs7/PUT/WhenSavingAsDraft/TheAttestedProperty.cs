using Bard;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Model.ReferenceData;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSavingAsDraft
{
    public class TheAttestedProperty : IntegrationTestBase<ECEStoryBook, ECEStoryData>
    {
        private const string Url = "api/rs7";

        public TheAttestedProperty(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private static void ClearAllDay(Rs7Model rs7)
        {
            if (rs7.AdvanceMonths == null) return;
            foreach (var rs7AdvanceMonthModel in rs7.AdvanceMonths) rs7AdvanceMonthModel.AllDay = null;
        }

        [Theory]
        [InlineData(OrganisationType.FreeKindergarten)]
        [InlineData(OrganisationType.HomebasedNetwork)]
        [InlineData(OrganisationType.Playcentre)]
        [InlineData(OrganisationType.TeKohangaReo)]
        [InlineData(OrganisationType.CasualEducationAndCare)]
        [InlineData(OrganisationType.EducationAndCare)]
        [InlineData(OrganisationType.Hospitalbased)]
        public void ForTheseOrganisationTypesTheIsAttestedFieldIsNotRequired(int organisationType)
        {
            // Arrange 
            var rs7Created = new Rs7Created();

            Given
                .An_rs7_has_been_created_for_an_organisation_type(organisationType)
                .GetResult(created => rs7Created = created.Rs7Created);

            var command = ModelBuilder.SaveAsDraft(rs7Created, rs7 =>
            {
                rs7.IsAttested = null;
                ClearAllDay(rs7);
            });

            // Act
            When.Put($"{Url}/{rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .NoContent();
        }
    }
}