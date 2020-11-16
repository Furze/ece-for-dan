using System.Collections.Generic;
using MoE.ECE.Domain.Infrastructure.Services.Opa;
using Newtonsoft.Json;

namespace MoE.ECE.Domain.Services.Opa.Response
{
    public class OperationalFundingBaseResponse
    {
        [JsonProperty(PropertyName = "@id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "fundingYear")]
        public int FundingYear { get; set; }

        [JsonProperty(PropertyName = "fundingPeriod")]
        public int FundingPeriod { get; set; }

        [JsonProperty(PropertyName = "eceTotalWashUp")]
        public decimal? TotalWashUp { get; set; }

        [JsonProperty(PropertyName = "eceTotalAdvance")]
        public decimal? TotalAdvance { get; set; }

        [JsonProperty(PropertyName = "isLicenceExempt")]
        public bool? IsLicenceExempt { get; set; }

        [JsonProperty(PropertyName = "relEntitlementMonth")]
        public ICollection<EntitlementMonth>? EntitlementMonths { get; set; }

        [JsonProperty(PropertyName = "relAdvanceMonth")]
        public ICollection<AdvanceMonth>? AdvanceMonths { get; set; }

        [JsonProperty(PropertyName = "@errors")]
        public ICollection<OpaError>? Errors { get; set; }
    }
}
