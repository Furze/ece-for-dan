using System.Collections.Generic;

namespace MoE.ECE.Domain.Read.Model.OperationalFunding
{
    public class OperationalFundingRequestModel : FundingRequestModel
    {
        public ICollection<EntitlementMonthFundingComponentModel>? EntitlementMonths { get; set; }
    }
}