using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSavingAsDraft
{
    public class TheAttestedProperty : IntegrationTestBase
    {
        public TheAttestedProperty(
            RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output,
            TestState testState)
            : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";
       
        private static void ClearAllDay(Rs7Model rs7)
        {
            if (rs7.AdvanceMonths == null) return;
            foreach (var rs7AdvanceMonthModel in rs7.AdvanceMonths)
            {
                rs7AdvanceMonthModel.AllDay = null;
            }
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

            If
                .An_rs7_has_been_created_for_an_organisation_type(organisationType)
                .UseResult(created => rs7Created = created);

            var command = Command.SaveAsDraft(rs7Created, rs7 =>
            {
                rs7.IsAttested = null;
                ClearAllDay(rs7);
            });

            // Act
            Api.Put($"{Url}/{rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .NoContent();
        }
    }
}