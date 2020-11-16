using Newtonsoft.Json;

namespace MoE.ECE.Domain.Services.Opa.Request
{
    public class EntitlementDay
    {
        [JsonProperty(PropertyName = "@id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "dayNameNumber")]
        public int? Number { get; set; }

        [JsonProperty(PropertyName = "dayEntitlementFCHUnder2")]
        public int? FCHUnder2 { get; set; }

        [JsonProperty(PropertyName = "dayEntitlementFCH2AndOver")]
        public int? FCH2AndOver { get; set; }

        [JsonProperty(PropertyName = "dayEntitlementFCH20Hours")]
        public int? FCH20Hours { get; set; }

        [JsonProperty(PropertyName = "dayEntitlementFCHPlus10")]
        public int? FCHPlus10 { get; set; }

        [JsonProperty(PropertyName = "dayCertificatedTeacherHours")]
        public int? CertificatedTeacherHours { get; set; }

        [JsonProperty(PropertyName = "dayNonCertificatedTeacherHours")]
        public int? NonCertificatedTeacherHours { get; set; }
    }
}