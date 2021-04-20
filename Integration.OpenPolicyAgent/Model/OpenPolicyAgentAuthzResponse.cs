using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MoE.ECE.Integration.OpenPolicyAgent.Model
{
    public class OpenPolicyAgentAuthzResponse
    {
        [JsonProperty("allow")]
        public bool Allow { get; set; }

        [JsonProperty("position_id")]
        public string PositionId { get; set; } = "";

        [JsonProperty("org")]
        public string Organisation { get; set; } = "";

        [JsonProperty("delegations_financial")]
        public List<JObject> FinancialDelegations { get; set; } = new List<JObject>();

        [JsonProperty("teams")]
        public List<string> Teams { get; set; } = new List<string>();
    }
}