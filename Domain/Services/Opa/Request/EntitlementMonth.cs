using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoE.ECE.Domain.Services.Opa.Request
{
    public class EntitlementMonth
    {
        [JsonProperty(PropertyName = "@id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "entMonthNumber")]
        public int MonthNumber { get; set; }

        [JsonProperty(PropertyName = "entMonthFundsAdvancedUnder2")]
        public decimal? MonthFundsAdvancedUnder2 { get; set; }

        [JsonProperty(PropertyName = "entMonthFundsAdvanced2AndOver")]
        public decimal? MonthFundsAdvanced2AndOver { get; set; }

        [JsonProperty(PropertyName = "entMonthFundsAdvanced20Hours")]
        public decimal? MonthFundsAdvanced20Hours { get; set; }

        [JsonProperty(PropertyName = "entMonthFundsAdvancedPlus10")]
        public decimal? MonthFundsAdvancedPlus10 { get; set; }

        [JsonProperty(PropertyName = "relEntitlementDay")]
        public ICollection<EntitlementDay>? EntitlementDays { get; set; }
    }
}