using System;
using System.Collections.Generic;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Model.OperationalFunding
{
    public abstract class FundingRequest : BusinessEntity
    {
        public int OrganisationId { get; set; }

        public FundingPeriodMonth? FundingPeriodMonth { get; set; }

        public abstract string FundingDescription { get; }

        public int FundingYear { get; set; }

        public int RevisionNumber { get; set; }

        public decimal? TotalWashUp { get; set; }

        public decimal? TotalAdvance { get; set; }

        public string AssignedTo { get; set; } = string.Empty;

        public DateTimeOffset? AssignedOn { get; set; }

        public string Status { get; set; } = string.Empty;

        public ICollection<AdvanceMonthFundingComponent> AdvanceMonths { get; set; } = new List<AdvanceMonthFundingComponent>();

        public DateTimeOffset CreationDate { get; set; }
        
        public string? RequestId { get; set; }
    }
}