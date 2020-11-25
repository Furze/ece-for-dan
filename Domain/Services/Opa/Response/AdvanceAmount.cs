using System;
using Newtonsoft.Json;

namespace MoE.ECE.Domain.Services.Opa.Response
{
    public class AdvanceAmount
    {
        [JsonProperty(PropertyName = "@id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "advanceAmountStartDate")]
        public DateTime? StartDate { get; set; }

        [JsonProperty(PropertyName = "advanceAmountComponentType")]
        public string? ComponentType { get; set; }

        [JsonProperty(PropertyName = "advanceAmountName")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "advanceAmountSessionType")]
        public string? SessionType { get; set; }

        [JsonProperty(PropertyName = "advanceAmountDays")]
        public int? Days { get; set; }

        [JsonProperty(PropertyName = "advanceAmountRate")]
        public decimal? Rate { get; set; }

        [JsonProperty(PropertyName = "advanceAmountRateName")]
        public string? RateName { get; set; }

        [JsonProperty(PropertyName = "advanceAmount")]
        public decimal? Amount { get; set; }

        [JsonProperty(PropertyName = "advanceAmountFCH")]
        public int? Fch { get; set; }
    }
}