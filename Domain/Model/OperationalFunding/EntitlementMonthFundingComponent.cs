using System.Collections.Generic;

namespace MoE.ECE.Domain.Model.OperationalFunding
{
    public class EntitlementMonthFundingComponent : DomainEntity
    {
        public FundingRequest OperationalFunding { get; set; } = new OperationalFundingRequest();

        public string? MonthName { get; set; }

        public int? MonthNumber { get; set; }

        public int? Year { get; set; }

        public int? AllDayCertificatedTeacherHours { get; set; }

        public int? AllDayNonCertificatedTeacherHours { get; set; }

        public int? SessionalCertificatedTeacherHours { get; set; }

        public int? SessionalNonCertificatedTeacherHours { get; set; }

        public int? TotalWorkingDays { get; set; }

        public decimal? TotalEntitlement { get; set; }

        public decimal? TotalFundsAdvanced { get; set; }

        public decimal? TotalEntitlementUnderTwo { get; set; }

        public decimal? TotalEntitlementTwoAndOver { get; set; }

        public decimal? TotalEntitlementTwentyHours { get; set; }

        public decimal? TotalEntitlementPlusTen { get; set; }

        public decimal? WashUpPlusTen { get; set; }

        public decimal? WashUpUnderTwo { get; set; }

        public decimal? WashUpTwoAndOver { get; set; }

        public decimal? WashUpTwentyHours { get; set; }

        public decimal? TotalWashUp { get; set; }

        public ICollection<EntitlementFundingComponent> FundingComponents { get; set; } = new List<EntitlementFundingComponent>();
    }
}