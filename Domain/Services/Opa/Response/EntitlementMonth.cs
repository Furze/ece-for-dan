using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoE.ECE.Domain.Services.Opa.Response
{
    public class EntitlementMonth
    {
        [JsonProperty(PropertyName = "@id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "entMonthNumber")]
        public int MonthNumber { get; set; }

        [JsonProperty(PropertyName = "entMonthName")]
        public string? MonthName { get; set; }

        [JsonProperty(PropertyName = "entMonthYear")]
        public int? Year { get; set; }

        //Totals
        [JsonProperty(PropertyName = "entMonthTotalEntitlement")]
        public decimal? TotalEntitlement { get; set; }

        [JsonProperty(PropertyName = "entMonthTotalFundsAdvanced")]
        public decimal? TotalFundsAdvanced { get; set; }

        [JsonProperty(PropertyName = "entMonthEntitlementUnder2")]
        public decimal? TotalEntitlementUnderTwo { get; set; }

        [JsonProperty(PropertyName = "entMonthEntitlement2andOver")]
        public decimal? TotalEntitlementTwoAndOver { get; set; }

        [JsonProperty(PropertyName = "entMonthEntitlement20Hours")]
        public decimal? TotalEntitlementTwentyHours { get; set; }

        [JsonProperty(PropertyName = "entMonthEntitlementPlus10")]
        public decimal? TotalEntitlementPlusTen { get; set; }

        //Washup
        [JsonProperty(PropertyName = "entMonthWashUpPlus10")]
        public decimal? WashUpPlusTen { get; set; }

        [JsonProperty(PropertyName = "entMonthWashUpUnder2")]
        public decimal? WashUpUnderTwo { get; set; }

        [JsonProperty(PropertyName = "entMonthWashUp2AndOver")]
        public decimal? WashUpTwoAndOver { get; set; }

        [JsonProperty(PropertyName = "entMonthWashUp20Hours")]
        public decimal? WashUpTwentyHours { get; set; }

        [JsonProperty(PropertyName = "entMonthTotalWashUp")]
        public decimal? TotalWashUp { get; set; }

        //Teacher hours
        [JsonProperty(PropertyName = "entMonthAllDayCertificatedTeacherHours")]
        public int? AllDayCertificatedTeacherHours { get; set; }

        [JsonProperty(PropertyName = "entMonthSessionalCertificatedTeacherHours")]
        public int? SessionalCertificatedTeacherHours { get; set; }

        [JsonProperty(PropertyName = "entMonthSessionalNonCertificatedTeacherHours")]
        public int? SessionalNonCertificatedTeacherHours { get; set; }

        [JsonProperty(PropertyName = "entMonthAllDayNonCertificatedTeacherHours")]
        public int? AllDayNonCertificatedTeacherHours { get; set; }

        //Other
        [JsonProperty(PropertyName = "entMonthTotalWorkingDays")]
        public int? TotalWorkingDays { get; set; }

        [JsonProperty(PropertyName = "relEntitlementAmount")]
        public ICollection<EntitlementAmount>? EntitlementAmounts { get; set; }
    }
}