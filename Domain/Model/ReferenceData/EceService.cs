using System;
using System.Collections.Generic;
using System.Linq;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceService
    {
       public int RefOrganisationId { get; set; }
       public string OrganisationName { get; set; } = string.Empty;
        public string OrganisationNumber { get; set; } = string.Empty;
        public int OrganisationTypeId { get; set; }
        public string? OrganisationTypeDescription { get; set; }
        public int? OrganisationSectorRoleId { get; set; }
        public string? OrganisationSectorRoleDescription { get; set; }
        public int OrganisationStatusId { get; set; }
        public string? OrganisationStatusDescription { get; set; }
        public string? ExternalProviderId { get; set; }
        public long? Nzbn { get; set; }
        public int? RegionId { get; set; }
        public string? RegionDescription { get; set; }

        // Fields from LicensedECEService (except those already in Organisation)
        // LocalOfficeId added here rather than in the Organisation section since the other 'LocalOffice' fields are here
        public DateTimeOffset? OpenDate { get; set; }
        public DateTimeOffset? EarliestOpenDate { get; set; }
        public DateTimeOffset? EceServiceStatusDate { get; set; }
        public int? EceServiceStatusReasonId { get; set; }
        public string? EceServiceStatusReasonDescription { get; set; }
        public int? PhoneLocatorId { get; set; }
        public string? PhoneNumber { get; set; }
        public int? FaxLocatorId { get; set; }
        public string? FaxNumber { get; set; }
        public int? EmailLocatorId { get; set; }
        public string? Email { get; set; }
        public int? OtherEmailLocatorId { get; set; }
        public string? OtherEmail { get; set; }
        public int? WebsiteLocatorId { get; set; }
        public string? Website { get; set; }
        public int? LocalOfficeId { get; set; }
        public string? LocalOfficeNumber { get; set; }
        public string? LocalOfficeName { get; set; }
        public string? EducationPhilosophyIds { get; set; }
        public string? EducationPhilosophyDescriptions { get; set; }
        public string? ReligiousAffiliationIds { get; set; }
        public string? ReligiousAffiliationDescriptions { get; set; }
        public string? EceCulturalCharacterIds { get; set; }
        public string? EceCulturalCharacterDescriptions { get; set; }
        public string? SpecialistServiceIds { get; set; }
        public string? SpecialistServiceDescriptions { get; set; }
        public int? SeDistrictId { get; set; }
        public string? SeDistrictDescription { get; set; }
        public string? MoeContact { get; set; }
        public string? MoeContactName { get; set; }
        public int? PrimaryLanguageId { get; set; }
        public string? PrimaryLanguageDescription { get; set; }
        public bool? IsFunded { get; set; }
        public DateTimeOffset? FundingDate { get; set; }
        public string? NotFundedReasonIds { get; set; }
        public string? NotFundedReasonDescriptions { get; set; }
        public DateTimeOffset? ClosedFunding { get; set; }
        public int? MeshBlockId { get; set; }
        public int? MeshBlockNumber { get; set; }
        public int? AreaUnitId { get; set; }
        public string? AreaUnitName { get; set; }
        public int? GeneralElectoralDistrictId { get; set; }
        public string? GeneralElectoralDistrictName { get; set; }
        public int? MaoriElectoralDistrictId { get; set; }
        public string? MaoriElectoralDistrictName { get; set; }
        public int? RegionalCouncilId { get; set; }
        public string? RegionalCouncilName { get; set; }
        public int? TerritorialAuthorityId { get; set; }
        public string? TerritorialAuthorityName { get; set; }
        public int? UrbanAreaId { get; set; }
        public string? UrbanAreaName { get; set; }
        public int? WardId { get; set; }
        public string? WardName { get; set; }
        public int? EceServiceProviderId { get; set; }
        public string? EceServiceProviderNumber { get; set; }
        public string? EceServiceProviderName { get; set; }
        public int? LocationShortAddressId { get; set; }
        public string? LocationAddressLine1 { get; set; }
        public string? LocationAddressLine2 { get; set; }
        public string? LocationAddressLine3 { get; set; }
        public string? LocationAddressLine4 { get; set; }
        public int? PostalShortAddressId { get; set; }
        public string? PostalAddressLine1 { get; set; }
        public string? PostalAddressLine2 { get; set; }
        public string? PostalAddressLine3 { get; set; }
        public string? PostalAddressLine4 { get; set; }

        // Fields from LicencingDetail (except those already in Organisation and LicensedECEService)
        public int? ServiceProvisionTypeId { get; set; }
        public string? ServiceProvisionTypeDescription { get; set; }
        public int ApplicationStatusId { get; set; }
        public string? ApplicationStatusDescription { get; set; }
        public int LicenceStatusId { get; set; }
        public string? LicenceStatusDescription { get; set; }
        public int? LicenceClassId { get; set; }
        public string? LicenceClassDescription { get; set; }
        public int? FundingContactId { get; set; }

        // Fields from ECEResourcingParameters (except those already in Organisation)
        public int? BulkFundingRateId { get; set; }
        public string? BulkFundingRateDescription { get; set; }
        public int? EquityIndexId { get; set; }
        public string? EquityIndexDescription { get; set; }
        public decimal? IsolationIndex { get; set; }
        public int? OtherLanguageId { get; set; }
        public string? OtherLanguageDescription { get; set; }
        public bool? InstallmentPayments { get; set; }
        public bool? WithholdPayments { get; set; }
        public string? InstallmentPaymentReasonIds { get; set; }
        public string? InstallmentPaymentReasonDescriptions { get; set; }
        public string? InstallmentPaymentWithheldReasonIds { get; set; }
        public string? InstallmentPaymentWithheldReasonDescriptions { get; set; }
        public bool? ProtectedRate { get; set; }
        public int? EcQualityLevelId { get; set; }
        public string? EcQualityLevelDescription { get; set; }
        public bool? TeacherLedEligibleToOfferFree { get; set; }
        public bool? ParentLedEligibleToOfferFree { get; set; }
        public bool? BlockedFromOfferingFreeEce { get; set; }
        public bool? IsPoIndicator { get; set; }
        public bool? IsNotionalRoleUsed { get; set; }

        // Selective fields from ECEServiceProvider (have added the EceServiceProvider prefix)
        public int? EceServiceProviderOwnershipTypeId { get; set; }
        public string? EceServiceProviderOwnershipTypeDescription { get; set; }

        public virtual ICollection<EceOperatingSession> OperatingSessions { get; set; } = null!;

        public bool IsAttestationRequired =>
            OrganisationTypeId == OrganisationType.CasualEducationAndCare ||
            OrganisationTypeId == OrganisationType.EducationAndCare ||
            OrganisationTypeId == OrganisationType.Hospitalbased;
        
        public bool IsParentLed 
        {
            get
            {
                return OrganisationTypeId switch
                {
                    OrganisationType.Playcentre => LicenceClassId != LicenceClass.Mixed,
                    _ => false
                };
            }
        }

        public bool IsAllDays
        {
            get
            {
                return OrganisationTypeId switch
                {
                    OrganisationType.CasualEducationAndCare => LicenceClassId != LicenceClass.Sessional,
                    OrganisationType.EducationAndCare => LicenceClassId != LicenceClass.Sessional,
                    OrganisationType.Hospitalbased => LicenceClassId != LicenceClass.Sessional,
                    OrganisationType.FreeKindergarten => LicenceClassId != LicenceClass.Sessional,
                    OrganisationType.Playcentre => false,
                    OrganisationType.HomebasedNetwork => true,
                    _ => true
                };
            }
        }
        
        public bool IsSessional
        {
            get
            {
                return OrganisationTypeId switch
                {
                    OrganisationType.CasualEducationAndCare => LicenceClassId != LicenceClass.AllDay,
                    OrganisationType.EducationAndCare => LicenceClassId != LicenceClass.AllDay,
                    OrganisationType.Hospitalbased => LicenceClassId != LicenceClass.AllDay,
                    OrganisationType.FreeKindergarten => LicenceClassId != LicenceClass.AllDay,
                    OrganisationType.Playcentre => false,
                    OrganisationType.HomebasedNetwork => false,
                    _ => true
                };
            }
        }

        public int ParentLedMaxFundingDays(int month, int year)
        {
            return CalculateMaximumFundingDays(IsParentLed, month, year, SessionType.Sessional);
        }
        
        public int AllDaysMaxFundingDays(int month, int year)
        {
            return CalculateMaximumFundingDays(IsAllDays, month, year, SessionType.AllDay);
        }
        
        public int SessionalMaxFundingDays(int month, int year)
        {
            return CalculateMaximumFundingDays(IsSessional, month, year, SessionType.Sessional);
        }
        
        public int CalculateMaximumFundingDays(bool isServiceEligible,
            int month, int year,
            int sessionTypeId
        )
        {
            if (isServiceEligible == false) return 0;
            if (month < 1 || month > 12 || year < 1 || year > 9999) return 0;
            
            var numberOfDaysInMonth = DateTime.DaysInMonth(year, month);
            
            var daysOfWeekWithSessions = OperatingSessions.Where(operatingSession =>
                operatingSession.SessionTypeId == sessionTypeId)
                .ToArray();
            
            var availableDays = 0;

            if (!daysOfWeekWithSessions.Any()) return availableDays;
            
            for (var dayOfMonth = 1; dayOfMonth <= numberOfDaysInMonth; dayOfMonth++)
            {
                // Get the day of the week eg. Sunday = 0, Monday = 1 etc..
                var dayOfWeek = new DateTime(
                    year,
                    month,
                    dayOfMonth
                ).DayOfWeek;

                // Check whether the day of the week is allowed a session on that day of the week.
                if (daysOfWeekWithSessions.Any(session => session.DayOfWeek == dayOfWeek))
                    availableDays++;
            }

            return availableDays;
        }
    }
}
