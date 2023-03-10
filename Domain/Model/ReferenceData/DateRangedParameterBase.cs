using System;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public abstract class DateRangedParameterBase
    {
        public int HistoryId { get; set; }
        public int RefOrganisationId { get; set; }
        public string Attribute { get; set; } = null!;
        public string AttributeSource { get; set; } = null!;
        public string? Value { get; set; } = null!;
        public string? ValueDescription { get; set; }
        public bool IsArray { get; set; }
        public DateTimeOffset EffectiveFromDate { get; set; }
        public DateTimeOffset? EffectiveToDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
