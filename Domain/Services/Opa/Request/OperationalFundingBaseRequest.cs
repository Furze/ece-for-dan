using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoE.ECE.Domain.Services.Opa.Request
{
    public class OperationalFundingBaseRequest
    {
        [JsonProperty(PropertyName = "@id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "serviceIsAttested")]
        public string? IsAttested { get; set; }

        public string? ApplicationType { get; set; }

        public int FundingYear { get; set; }

        public int? FundingPeriod { get; set; }

        [JsonProperty(PropertyName = "relServiceProfile")]
        public ICollection<ServiceProfile>? ServiceProfiles { get; set; }

        [JsonProperty(PropertyName = "relEntitlementMonth")]
        public ICollection<EntitlementMonth>? EntitlementMonths { get; set; }

        [JsonProperty(PropertyName = "relAdvanceMonth")]
        public ICollection<AdvanceMonth>? AdvanceMonths { get; set; }
    }

    public class ServiceProfile
    {
        [JsonProperty(PropertyName = "@id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "codeValueDescription")]
        public string? Description { get; set; }

        [JsonProperty(PropertyName = "codeValueValueID")]
        public string? ValueID { get; set; }

        [JsonProperty(PropertyName = "codeValueEffectiveFromDate")]
        public DateTimeOffset? EffectiveFromDate { get; set; }

        [JsonProperty(PropertyName = "codeValueEffectiveToDate")]
        public DateTimeOffset? EffectiveToDate { get; set; }
    }
}