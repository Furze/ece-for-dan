using System.Collections.Generic;

namespace Moe.ECE.Events.Integration.ELI
{
    public class Rs7ReceivedEntitlementMonth
    {
        public int? MonthNumber { get; set; }

        public int FundingPeriodYear { get; set; }

        public ICollection<Rs7ReceivedEntitlementDay>? Days { get; set; }
    }
}