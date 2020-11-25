using System;
using Newtonsoft.Json;

namespace MoE.ECE.Domain.Services.Opa.Response
{
    public class EntitlementAmount
    {
        [JsonProperty(PropertyName = "@id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "entitlementAmountSessionType")]
        public string? SessionType { get; set; }

        [JsonProperty(PropertyName = "entitlementAmountStartDate")]
        public DateTime? StartDate { get; set; }

        [JsonProperty(PropertyName = "entitlementAmountRateName")]
        public string? RateName { get; set; }

        [JsonProperty(PropertyName = "entitlementAmountRate")]
        public decimal? Rate { get; set; }

        [JsonProperty(PropertyName = "entitlementAmountFCH")]
        public int? Fch { get; set; }

        [JsonProperty(PropertyName = "entitlementAmountDays")]
        public int? Days { get; set; }

        [JsonProperty(PropertyName = "entitlementAmountName")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "entitlementAmount")]
        public decimal? Amount { get; set; }

        [JsonProperty(PropertyName = "entitlementAmountComponentType")]
        public string? ComponentType { get; set; }
    }
}