using System;
using System.Collections.Generic;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceService
    {
        public int RefOrganisationId { get; set; }
        public string OrganisationName { get; set; } = null!;
        public string OrganisationNumber { get; set; } = null!;
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
        public DateTimeOffset? OpenDate { get; set; }
        public DateTimeOffset? EceServiceStatusDate { get; set; }
        public int? EceServiceStatusReasonId { get; set; }
        public string? EceServiceStatusReasonDescription { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FaxNumber { get; set; }
        public string? Email { get; set; }
        public string? OtherEmail { get; set; }
        public string? Website { get; set; }
        public int? PrimaryLanguageId { get; set; }
        public string? PrimaryLanguageDescription { get; set; }
        public bool? IsFunded { get; set; }
        public int? EceServiceProviderId { get; set; }
        public string? EceServiceProviderNumber { get; set; }
        public string? EceServiceProviderName { get; set; }
        public string? LocationAddressLine1 { get; set; }
        public string? LocationAddressLine2 { get; set; }
        public string? LocationAddressLine3 { get; set; }
        public string? LocationAddressLine4 { get; set; }
        public string? PostalAddressLine1 { get; set; }
        public string? PostalAddressLine2 { get; set; }
        public string? PostalAddressLine3 { get; set; }
        public string? PostalAddressLine4 { get; set; }
        public int? ServiceProvisionTypeId { get; set; }
        public string? ServiceProvisionTypeDescription { get; set; }
        public int ApplicationStatusId { get; set; }
        public string? ApplicationStatusDescription { get; set; }
        public int LicenceStatusId { get; set; }
        public string? LicenceStatusDescription { get; set; }
        public int? LicenceClassId { get; set; }
        public string? LicenceClassDescription { get; set; }
        public int? EquityIndexId { get; set; }
        public string? EquityIndexDescription { get; set; }
        public decimal? IsolationIndex { get; set; }
        public bool? InstallmentPayments { get; set; }
        public int? EcQualityLevelId { get; set; }
        public string? EcQualityLevelDescription { get; set; }
        public bool? TeacherLedEligibleToOfferFree { get; set; }
        public bool? ParentLedEligibleToOfferFree { get; set; }
        public int? EceServiceProviderOwnershipTypeId { get; set; }
        public string? EceServiceProviderOwnershipTypeDescription { get; set; }

        public virtual ICollection<EceOperatingSession> OperatingSessions { get; set; } = new List<EceOperatingSession>();
    }
}
