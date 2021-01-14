using System.Collections.Generic;

namespace MoE.ECE.Domain.Model.OperationalFunding
{
    public class AdvanceMonthFundingComponent 
    {
        public string? MonthName { get; set; }

        public int? MonthNumber { get; set; }

        public int? Year { get; set; }

        public int? AllDayWorkingDays { get; set; }

        public int? SessionalWorkingDays { get; set; }

        public int? ParentLedWorkingDays { get; set; }

        public decimal? AmountPayablePlusTen { get; set; }

        public decimal? AmountPayableUnderTwo { get; set; }

        public decimal? AmountPayableTwoAndOver { get; set; }

        public decimal? AmountPayableTwentyHours { get; set; }

        public int? TotalDays { get; set; }

        public decimal? TotalAdvance { get; set; }

        public virtual ICollection<AdvanceFundingComponent> AdvanceFundingComponents { get; set; } = new List<AdvanceFundingComponent>();
    }
}