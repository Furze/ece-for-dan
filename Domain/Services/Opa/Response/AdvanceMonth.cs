using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoE.ECE.Domain.Services.Opa.Response
{
    public class AdvanceMonth
    {
        [JsonProperty(PropertyName = "@id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "advMonthNumber")]
        public int? MonthNumber { get; set; }

        [JsonProperty(PropertyName = "advMonthName")]
        public string? MonthName { get; set; }

        [JsonProperty(PropertyName = "advMonthYear")]
        public int? Year { get; set; }

        [JsonProperty(PropertyName = "advMonthEstimateSessionalDays")]
        public int? EstimateSessionalDays { get; set; }

        [JsonProperty(PropertyName = "advMonthEstimateAllDayDays")]
        public int? EstimateAllDayDays { get; set; }

        [JsonProperty(PropertyName = "advMonthEstimateParentLedDays")]
        public int? EstimateParentLedDays { get; set; }

        [JsonProperty(PropertyName = "advMonthAmountPayableUnder2")]
        public decimal? PayableUnderTwo { get; set; }

        [JsonProperty(PropertyName = "advMonthAmountPayable2AndOver")]
        public decimal? PayableTwoAndOver { get; set; }

        [JsonProperty(PropertyName = "advMonthAmountPayable20Hours")]
        public decimal? PayableTwentyHours { get; set; }

        [JsonProperty(PropertyName = "advMonthAmountPayablePlus10")]
        public decimal? PayablePlusTen { get; set; }

        [JsonProperty(PropertyName = "relAdvanceAmount")]
        public ICollection<AdvanceAmount>? AdvanceAmounts { get; set; }

        [JsonProperty(PropertyName = "advMonthTotalDays")]
        public int? TotalDays { get; set; }

        [JsonProperty(PropertyName = "advMonthTotalAdvance")]
        public decimal? TotalAdvance { get; set; }
    }
}