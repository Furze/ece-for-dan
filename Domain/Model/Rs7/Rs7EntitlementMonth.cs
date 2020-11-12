using System.Collections.Generic;
using System.Linq;

namespace MoE.ECE.Domain.Model.Rs7
{
    public class Rs7EntitlementMonth
    {
        public int Id { get; set; }

        public int MonthNumber { get; set; }

        public int Year { get; set; }

        public int Rs7RevisionId { get; set; }

        public ICollection<Rs7EntitlementDay> Days = new List<Rs7EntitlementDay>();

        public void AddDay(Rs7EntitlementDay match)
        {
            Days.Add(match);
        }

        internal Rs7EntitlementMonth Clone()
        {
            return new Rs7EntitlementMonth
            {
                MonthNumber = MonthNumber,
                Year = Year,
                Days = Days.Select(d => d.Clone()).ToList()
            };
        }
    }
}