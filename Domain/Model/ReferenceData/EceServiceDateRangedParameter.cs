using System;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceServiceDateRangedParameter
    {
        public int HistoryId { get; set; }
        public int RefOrganisationId { get; set; }
        public string? Attribute { get; set; }
        public string AttributeSource { get; set; } = null!;
        public string? Value { get; set; }
        public string? ValueDescription { get; set; }
        public bool IsArray { get; set; }
        public DateTimeOffset EffectiveFromDate { get; set; }
        public DateTimeOffset? EffectiveToDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
