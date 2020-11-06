using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
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

        [Theory]
        [InlineData(OrganisationType.CasualEducationAndCare)]
        [InlineData(OrganisationType.EducationAndCare)]
        [InlineData(OrganisationType.Hospitalbased)]
        public void ForTheseOrganisationTypesTheIsAttestedFieldIsRequired(int organisationType)
        {
            // Arrange 
            var rs7Created = new Rs7Created();

            If
                .An_rs7_has_been_created_for_an_organisation_type(organisationType)
                .UseResult(created => rs7Created = created);

            // Act
            Api.Put($"{Url}/{rs7Created.Id}", Command.UpdateRs7(rs7Created, rs7 => 
            {
                rs7.RollStatus = RollStatus.InternalReadyForReview;
                rs7.IsAttested = null; 
            }));

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<UpdateRs7>(rs7 => rs7.IsAttested)
                .WithMessage("'Is Attested' must not be empty.");
        }

        /// <summary>
        /// JIRA ERST-11196 validator incorrectly checking that the field is true rather than not null!!
        /// </summary>
        /// <param name="organisationType"></param>
        [Theory]
        [InlineData(OrganisationType.CasualEducationAndCare)]
        [InlineData(OrganisationType.EducationAndCare)]
        [InlineData(OrganisationType.Hospitalbased)]
        public void ForTheseOrganisationTypesTheIsAttestedFieldCanBeSetToFalse(int organisationType)
        {
            // Arrange 
            var rs7Created = new Rs7Created();

            If
                .An_rs7_has_been_created_for_an_organisation_type(organisationType)
                .UseResult(created => rs7Created = created);

            // Act
            Api.Put($"{Url}/{rs7Created.Id}", Command.UpdateRs7(rs7Created, rs7 =>
            {
                rs7.RollStatus = RollStatus.InternalReadyForReview;
                rs7.IsAttested = false;
                ClearAllDay(rs7);
            }));

            Then.TheResponse
                .ShouldBe
                .NoContent();
        }

        private static void ClearAllDay(UpdateRs7 rs7)
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
        public void ForTheseOrganisationTypesTheIsAttestedFieldIsNotRequired(int organisationType)
        {
            // Arrange 
            var rs7Created = new Rs7Created();

            If
                .An_rs7_has_been_created_for_an_organisation_type(organisationType)
                .UseResult(created => rs7Created = created);

            var command = Command.UpdateRs7(rs7Created, rs7 =>
            {
                rs7.RollStatus = RollStatus.InternalReadyForReview;
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