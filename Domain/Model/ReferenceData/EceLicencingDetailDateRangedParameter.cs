using System;
using System.Collections.Generic;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceLicencingDetailDateRangedParameter
    {
        public int LicencingDetailHistoryId { get; set; }
        public int RefOrganisationId { get; set; }
        public int ApplicationStatusId { get; set; }
        public string? ApplicationStatusDescription { get; set; }
        public int LicenceStatusId { get; set; }
        public string? LicenceStatusDescription { get; set; }
        public int? LicenceClassId { get; set; }
        public string? LicenceClassDescription { get; set; }
        public int? ServiceProvisionTypeId { get; set; }
        public string? ServiceProvisionTypeDescription { get; set; }
        public DateTimeOffset EffectiveFromDate { get; set; }
        public DateTimeOffset? EffectiveToDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }

        public virtual EceService EceService { get; set; } = null!;
        public virtual ICollection<EceOperatingSessionDateRangedParameter> EceOperatingSessionDateRangedParameters { get; set; } = null!;
    }
}
