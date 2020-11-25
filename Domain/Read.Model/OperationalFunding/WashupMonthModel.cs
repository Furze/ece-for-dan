using System.Collections.Generic;

namespace MoE.ECE.Domain.Read.Model.OperationalFunding
{
    public class WashupMonthModel
    {
        public string? MonthName { get; set; }

        public int? MonthNumber { get; set; }

        public int? Year { get; set; }

        public int? TotalAdvanceDays { get; set; }

        public int? TotalEntitlementDays { get; set; }

        public decimal? WashUpPlusTen { get; set; }

        public decimal? WashUpUnderTwo { get; set; }

        public decimal? WashUpTwoAndOver { get; set; }

        public decimal? WashUpTwentyHours { get; set; }

        public decimal? TotalWashup { get; set; }

        public ICollection<WashupFundingComponentModel>? WashupFundingComponents { get; set; }
    }
}
