using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Read.Model.OperationalFunding
{
    public class WashupFundingComponentModel
    {
        //Advance
        public Session? AdvanceSessionTypeId { get; set; }

        public FundingComponent? AdvanceFundingComponentTypeId { get; set; }

        public int? AdvanceFundedChildHours { get; set; }

        public decimal? AdvanceRate { get; set; }

        public decimal? AdvanceAmount { get; set; }

        //Entitlement
        public Session? EntitlementSessionTypeId { get; set; }

        public FundingComponent? EntitlementFundingComponentTypeId { get; set; }

        public int? EntitlementFundedChildHours { get; set; }

        public decimal? EntitlementRate { get; set; }

        public decimal? EntitlementAmount { get; set; }
    }
}
