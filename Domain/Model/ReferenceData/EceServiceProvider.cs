using System.Collections.Generic;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceServiceProvider : OrganisationBase
    {
        public int? OwnershipTypeId { get; set; }
        public string? OwnershipTypeDescription { get; set; }
        public bool IsLicensedEceServiceProvider { get; set; }
        public bool IsPlaygroupServiceProvider { get; set; }
        public int NumberOfContacts { get; set; }
        public int NumberOfLicensedEceServices { get; set; }
        public int NumberOfPlaygroups { get; set; }

        public virtual ICollection<EceService> EceServices { get; set; } = new List<EceService>();
        public virtual ICollection<EceServiceProviderDateRangedParameter> EceServiceProviderDateRangedParameters { get; set; } = new List<EceServiceProviderDateRangedParameter>();
    }
}
