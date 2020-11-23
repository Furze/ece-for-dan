using System;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Model.OperationalFunding
{
    public abstract class ECEFundingComponent 
    {
        public Session? SessionTypeId { get; set; }

        public FundingComponent? FundingComponentTypeId { get; set; }

        public DateTime? StartDate { get; set; }

        public decimal? Amount { get; set; }

        public decimal? Rate { get; set; }

        public string? RateName { get; set; }

        public int? FundedChildHours { get; set; }

        public int? OperatingDays { get; set; }
    }
}