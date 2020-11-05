using System;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceOperatingSession
    {
        public int OperatingSessionId { get; set; }
        public int RefOrganisationId { get; set; }
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
        public int FundedHours { get; set; }
        public int OperatingHours { get; set; }

        public virtual EceService EceService { get; set; } = null!;
    }
}
