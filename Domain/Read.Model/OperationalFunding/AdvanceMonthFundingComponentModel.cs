using System.Collections.Generic;

namespace MoE.ECE.Domain.Read.Model.OperationalFunding
{
    public class AdvanceMonthFundingComponentModel
    {
        public string? MonthName { get; set; }

        public int? MonthNumber { get; set; }

        public int? Year { get; set; }

        public int? ParentLedWorkingDays { get; set; }

        public int? AllDayWorkingDays { get; set; }

        public int? SessionalWorkingDays { get; set; }

        public decimal? AmountPayablePlusTen { get; set; }

        public decimal? AmountPayableUnderTwo { get; set; }

        public decimal? AmountPayableTwoAndOver { get; set; }

        public decimal? AmountPayableTwentyHours { get; set; }

        public int? TotalDays { get; set; }

        public decimal? TotalAdvance { get; set; }

        public ICollection<AdvanceFundingComponentModel>? FundingComponents { get; set; }
    }
}
