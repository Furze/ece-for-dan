using Bard;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Model.ReferenceData;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
{
    public class TheAttestedProperty : IntegrationTestBase<ECEStoryBook, ECEStoryData>
    {
        private const string Url = "api/rs7";

        public TheAttestedProperty(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        [Theory]
        [InlineData(OrganisationType.CasualEducationAndCare)]
        [InlineData(OrganisationType.EducationAndCare)]
        [InlineData(OrganisationType.Hospitalbased)]
        public void ForTheseOrganisationTypesTheIsAttestedFieldIsRequired(int organisationType)
        {
            // Arrange 
            Rs7Model? rs7Created = new Rs7Model();

            Given
                .An_rs7_has_been_created_for_an_organisation_type(organisationType)
                .GetResult(created => rs7Created = created.Rs7Model);

            // Act
            When.Put($"{Url}/{rs7Created.Id}", ModelBuilder.UpdateRs7(rs7Created, rs7 =>
            {
                rs7.RollStatus = RollStatus.InternalReadyForReview;
                rs7.IsAttested = null;
            }));

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<UpdateRs7>(rs7 => rs7.IsAttested)
                .WithMessage("'Is Attested' must not be empty.");
        }

        /// <summary>
        ///     JIRA ERST-11196 validator incorrectly checking that the field is true rather than not null!!
        /// </summary>
        /// <param name="organisationType"></param>
        [Theory]
        [InlineData(OrganisationType.CasualEducationAndCare)]
        [InlineData(OrganisationType.EducationAndCare)]
        [InlineData(OrganisationType.Hospitalbased)]
        public void ForTheseOrganisationTypesTheIsAttestedFieldCanBeSetToFalse(int organisationType)
        {
            // Arrange 
            Rs7Model? rs7Created = new Rs7Model();

            Given
                .An_rs7_has_been_created_for_an_organisation_type(organisationType)
                .GetResult(created => rs7Created = created.Rs7Model);

            // Act
            When.Put($"{Url}/{rs7Created.Id}", ModelBuilder.UpdateRs7(rs7Created, rs7 =>
            {
                rs7.RollStatus = RollStatus.InternalReadyForReview;
                rs7.IsAttested = false;
                ClearAllDay(rs7);
            }));

            Then.Response
                .ShouldBe
                .NoContent();
        }

        private static void ClearAllDay(UpdateRs7 rs7)
        {
            if (rs7.AdvanceMonths == null)
            {
                return;
            }

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
            Rs7Model? rs7Created = new Rs7Model();

            Given
                .An_rs7_has_been_created_for_an_organisation_type(organisationType)
                .GetResult(created => rs7Created = created.Rs7Model);

            UpdateRs7? command = ModelBuilder.UpdateRs7(rs7Created, rs7 =>
            {
                rs7.RollStatus = RollStatus.InternalReadyForReview;
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