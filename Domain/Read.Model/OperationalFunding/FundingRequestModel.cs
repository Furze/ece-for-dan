using System;
using System.Collections.Generic;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Read.Model.OperationalFunding
{
    public class FundingRequestModel 
    {
        public int OrganisationId { get; set; }

        public Guid BusinessEntityId { get; set; } = Guid.NewGuid();
        
        public string? RequestId { get; set; }
        
        public FundingPeriodMonth? FundingPeriodMonth { get; set; }

        public int FundingYear { get; set; }

        public int RevisionNumber { get; set; }

        public decimal? TotalWashUp { get; set; }

        public decimal? TotalAdvance { get; set; }

        public ICollection<AdvanceMonthFundingComponentModel>? AdvanceMonths { get; set; }

        public ICollection<AdvanceMonthFundingComponentModel>? MatchingAdvanceMonths { get; set; }

        public ICollection<EquityFundingComponentModel>? Equity { get; set; }
    }
}