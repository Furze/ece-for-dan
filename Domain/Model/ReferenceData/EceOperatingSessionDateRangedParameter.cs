using System;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceOperatingSessionDateRangedParameter
    {
        public int RefOrganisationId { get; set; }
        public int LicencingDetailHistoryId { get; set; }
        public int OperatingSessionId { get; set; }
        public int? SessionDayId { get; set; }
        public string? SessionDayDescription { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public int? MaxChildren { get; set; }
        public int? MaxChildrenUnder2 { get; set; }
        public int? SessionTypeId { get; set; }
        public string? SessionTypeDescription { get; set; }
        public int? SessionProvisionTypeId { get; set; }
        public string? SessionProvisionTypeDescription { get; set; }
        public DateTimeOffset EffectiveFromDate { get; set; }
        public DateTimeOffset? EffectiveToDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }

        public virtual EceLicencingDetailDateRangedParameter EceLicencingDetailDateRangedParameter { get; set; } = null!;
    }
}
