using System;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class Lookup
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public int LookupTypeId { get; set; }
        public string? EdumisCode { get; set; }
        public int? ParentLookupId { get; set; }
        public DateTimeOffset EffectiveFromDate { get; set; }
        public DateTimeOffset? EffectiveToDate { get; set; }
    }
}
