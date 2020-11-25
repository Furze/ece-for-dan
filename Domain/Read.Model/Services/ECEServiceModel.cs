using System;
using System.Collections.Generic;

namespace MoE.ECE.Domain.Read.Model.Services
{
    public class ECEServiceModel : SearchEceServiceModel
    {
        public int ServiceTypeId { get; set; }

        public string? ServiceTypeDescription { get; set; }

        public int? LicenceClassId { get; set; }

        public string? LicenceClassDescription { get; set; }

        public int? LicenceStatusId { get; set; }
        
        public string? LicenceStatusDescription { get; set; }
        
        public int OrganisationStatusId { get; set; }
        
        public string? OrganisationStatusDescription { get; set; }

        public bool? InstallmentPayments { get; set; }
        
        public bool? IsFunded { get; set; }
        
        public int? ServiceProvisionTypeId { get; set; }

        public string? ServiceProvisionTypeDescription { get; set; }

        public int? PrimaryLanguageId { get; set; }

        public string? PrimaryLanguageDescription { get; set; }

        public int? OrganisationSectorRoleId { get; set; }
        
        public string? OrganisationSectorRoleDescription { get; set; }
        
        public DateTimeOffset? StatusDate { get; set; }
        
        public int? StatusReasonId { get; set; }
        
        public string? StatusReasonDescription { get; set; }
        
        public long? NZBN { get; set; }
        
        public DateTimeOffset? OpenDate { get; set; }
        
        public int? EceServiceProviderOwnershipTypeId { get; set; }
        
        public string? EceServiceProviderOwnershipTypeDescription { get; set; }
        
        public string? ExternalProviderId { get; set; }
        
        public string? PrimaryEmailAddress { get; set; }
        
        public string? OtherEmailAddress { get; set; }
        
        public string? PhoneNumber { get; set; }
        
        public string? WebSite { get; set; }
        
        public string? AddressLine1 { get; set; }
        
        public string? AddressLine2 { get; set; }
        
        public string? AddressLine3 { get; set; }
        
        public string? AddressLine4 { get; set; }
        
        public string? PostalAddressLine1 { get; set; }
        
        public string? PostalAddressLine2 { get; set; }
        
        public string? PostalAddressLine3 { get; set; }
        
        public string? PostalAddressLine4 { get; set; }
        
        public bool? TeacherLedEligibleToOfferFree { get; set; }

        public bool? ParentLedEligibleToOfferFree { get; set; }

        public bool CanClaimSubsidyFundedHours { get; set; }

        public bool CanClaimTeacherHours { get; set; }

        public bool CanClaim20ChildFundedHours { get; set; }

        public List<DailySessionModel> DailySessions { get; set; } = new List<DailySessionModel>();

        public List<DateTimeOffset> CreatableRs7FundingPeriods { get; set; } = new List<DateTimeOffset>();
    }
}