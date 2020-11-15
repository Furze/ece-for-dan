using System.Collections.Generic;

namespace MoE.ECE.Domain.Model.OperationalFunding
{
    public class OperationalFundingRequest : FundingRequest
    {
        public ICollection<EntitlementMonthFundingComponent>? EntitlementMonths { get; set; } = new List<EntitlementMonthFundingComponent>();

        public override string FundingDescription => "Existing Service Application for Funding (RS7)";
    }
}
