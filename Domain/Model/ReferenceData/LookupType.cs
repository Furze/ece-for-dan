using System.Collections.Generic;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class LookupType
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<Lookup> Lookups { get; set; } = null!;
    }
}
