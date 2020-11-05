using System.Collections.Generic;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public interface IRs7EntitlementMonths : IHasId
    {
        public IEnumerable<Rs7EntitlementMonthModel>? EntitlementMonths { get; set; }
    }
}