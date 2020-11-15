using System;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Read.Model.OperationalFunding
{
    public abstract class ECEFundingComponentModel
    {
        public Session? SessionType { get; set; }

        public FundingComponent? FundingComponentType { get; set; }

        public DateTime StartDate { get; set; }

        public decimal? Amount { get; set; }

        public decimal? Rate { get; set; }

        public string? RateName { get; set; }

        public int? FundedChildHours { get; set; }

        public int? OperatingDays { get; set; }
    }
}