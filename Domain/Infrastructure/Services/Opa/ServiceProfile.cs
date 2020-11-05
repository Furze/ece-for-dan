using System;
using Newtonsoft.Json;

namespace MoE.ECE.Domain.Infrastructure.Services.Opa
{
    public class ServiceProfile
    {
        [JsonProperty(PropertyName = "@id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "codeValueDescription")]
        public string? Description { get; set; }

        [JsonProperty(PropertyName = "codeValueValueID")]
        public string? ValueId { get; set; }

        [JsonProperty(PropertyName = "codeValueEffectiveFromDate")]
        public DateTimeOffset? EffectiveFromDate { get; set; }

        [JsonProperty(PropertyName = "codeValueEffectiveToDate")]
        public DateTimeOffset? EffectiveToDate { get; set; }
    }
}
