using System.Collections.Generic;

namespace Moe.ECE.Events.Integration.ELI
{
    /// <summary>
    /// Represents either a new - or a change to an existing - rs7 has been made.
    /// </summary>
    public class Rs7Received : IIntegrationEvent
    {
        public string OrganisationNumber { get; set; } = null!;

        public FundingPeriodMonth FundingPeriod { get; set; }

        public IEnumerable<Rs7ReceivedAdvanceMonth> AdvanceMonths { get; set; } = new HashSet<Rs7ReceivedAdvanceMonth>();

        public IEnumerable<Rs7ReceivedEntitlementMonth> EntitlementMonths { get; set; } = new HashSet<Rs7ReceivedEntitlementMonth>();

        public Rs7Declaration? Declaration { get; set; }

        public bool? IsAttested { get; set; }

        public string? Source { get; set; }
    }
}