using System.Collections.Generic;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceServiceProvider
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

        // Fields from EceServiceProvider
        public int? OwnershipTypeId { get; set; }
        public string? OwnershipTypeDescription { get; set; }
        public bool IsLicensedEceServiceProvider { get; set; }
        public bool IsPlaygroupServiceProvider { get; set; }
        public int NumberOfContacts { get; set; }
        public int NumberOfLicensedEceServices { get; set; }
        public int NumberOfPlaygroups { get; set; }

        public virtual ICollection<EceService> EceServices { get; set; } = new List<EceService>();
    }
}
