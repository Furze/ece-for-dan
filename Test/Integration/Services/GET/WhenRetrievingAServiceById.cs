using System.Linq;
using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Model.ReferenceData;
using MoE.ECE.Domain.Read.Model.Services;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Services.GET
{
    public class WhenRetrievingAServiceById : IntegrationTestBase<ECEStoryBook, ECEStoryData>
    {
        private readonly EceService _montessoriLittleHands = ReferenceData.EceServices.MontessoriLittleHands;

        

        [Fact]
        public void IfTheServiceExistsThenTheResultShouldBePopulatedCorrectly()
        {
            When.Get($"api/services/{ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId}");

            var result = Then.Response.ShouldBe.Ok<ECEServiceModel>();

            result.ServiceTypeDescription.ShouldBe(_montessoriLittleHands.OrganisationTypeDescription);
            result.ServiceTypeId.ShouldBe(_montessoriLittleHands.OrganisationTypeId);
            result.OrganisationNumber.ShouldBe(_montessoriLittleHands.OrganisationNumber);
            result.ServiceName.ShouldBe(_montessoriLittleHands.OrganisationName);
            result.ServiceProviderNumber.ShouldBe(_montessoriLittleHands.EceServiceProviderNumber);
            result.LicenceClassDescription.ShouldBe(_montessoriLittleHands.LicenceClassDescription);
            result.LicenceClassId.ShouldBe(_montessoriLittleHands.LicenceClassId);
            result.ServiceProvisionTypeDescription.ShouldBe(_montessoriLittleHands.ServiceProvisionTypeDescription);
            result.ServiceProvisionTypeId.ShouldBe(_montessoriLittleHands.ServiceProvisionTypeId);
            result.TeacherLedEligibleToOfferFree.ShouldBe(_montessoriLittleHands.TeacherLedEligibleToOfferFree);
            result.ParentLedEligibleToOfferFree.ShouldBe(_montessoriLittleHands.ParentLedEligibleToOfferFree);
            result.CanClaimTeacherHours.ShouldBe(true);
            result.CanClaim20ChildFundedHours.ShouldBe(true);
            result.CanClaimSubsidyFundedHours.ShouldBe(true);
            result.CreatableRs7FundingPeriods.Count.ShouldBe(5);
            result.OpenDate.ShouldNotBeNull();
        }

        [Fact]
        public void If_the_service_has_an_1858_date_then_it_should_be_set_to_null()
        {
            // When
            When.Get($"api/services/{ReferenceData.EceServices.LeestonPlaycentre.RefOrganisationId}");
            
            // Then
            var result = Then.Response.ShouldBe.Ok<ECEServiceModel>();
            
            result.OpenDate.ShouldBeNull();
            result.StatusDate.ShouldBeNull();
        }

        [Fact]
        public void IfTheServiceExistsThenTheDailySessionsShouldBePopulatedCorrectly()
        {
            When.Get($"api/services/{ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId}");

            var result = Then.Response.ShouldBe.Ok<ECEServiceModel>();

            result.DailySessions.ShouldNotBeEmpty();

            var mondaySession = result.DailySessions.SingleOrDefault(model => model.Day == SessionDay.Monday);

            mondaySession.ShouldNotBeNull();
            mondaySession?.DayOfWeek.ShouldBe(1);
            mondaySession?.FundedHours.ShouldBe(6);
            mondaySession?.OperatingHours.GetValueOrDefault().ShouldBe(9);
        }

        [Fact]
        public void IfTheServiceExistsThenTheOperatingTimesShouldBePopulatedCorrectly()
        {
            When.Get($"api/services/{ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId}");

            var result = Then.Response.ShouldBe.Ok<ECEServiceModel>();

            result.DailySessions.ShouldNotBeEmpty();

            var mondaySession = result.DailySessions.SingleOrDefault(model => model.Day == SessionDay.Monday);

            mondaySession.ShouldNotBeNull();

            mondaySession?.OperatingTimes.ShouldNotBeEmpty();
            mondaySession?.OperatingTimes.Count.ShouldBe(2);

            mondaySession?.OperatingTimes[0].StartTime.ShouldNotBeNull();
            mondaySession?.OperatingTimes[0].StartTime.Hour.ShouldBe(6);
            mondaySession?.OperatingTimes[0].StartTime.Minute.ShouldBe(30);

            mondaySession?.OperatingTimes[0].EndTime.ShouldNotBeNull();
            mondaySession?.OperatingTimes[0].EndTime.Hour.ShouldBe(12);
            mondaySession?.OperatingTimes[0].EndTime.Minute.ShouldBe(0);

            mondaySession?.OperatingTimes[0].MaxChildren.ShouldBe(35);
            mondaySession?.OperatingTimes[0].MaxChildrenUnder2.ShouldBe(8);
            
            
        }

        [Fact]
        public void IfTheServiceDoesNotExistThenTheResponseShouldBeA404()
        {
            When.Get($"api/services/23432"); // Random id that doesn't exist.

            Then.Response.ShouldBe.NotFound();
        }

        public WhenRetrievingAServiceById(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }
    }
}