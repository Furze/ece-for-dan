using System.Collections.Generic;
using System.Linq;

namespace MoE.ECE.Domain.Model.Rs7
{
    public class Rs7EntitlementMonth
    {
        private HashSet<Rs7EntitlementDay> _days = new HashSet<Rs7EntitlementDay>();

        public int Id { get; set; }

        public int MonthNumber { get; set; }

        public int Year { get; set; }

        public int Rs7RevisionId { get; set; }

        public virtual Rs7Revision Rs7Revision { get; set; } = null!;

        public virtual IEnumerable<Rs7EntitlementDay> Days => _days.OrderBy(day => day.DayNumber).ToList();

        public void AddDay(Rs7EntitlementDay match)
        {
            _days.Add(match);
        }

        internal Rs7EntitlementMonth Clone()
        {
            return new Rs7EntitlementMonth
            {
                MonthNumber = MonthNumber,
                Year = Year,
                _days = Days.Select(d => d.Clone()).ToHashSet()
            };
        }
    }
}