using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoE.ECE.Web.Infrastructure.HealthChecks
{
    public class Response
    {
        [JsonProperty("status")]
        public string Status { get; set; } = null!;
        [JsonProperty("name")]
        public string Name { get; set; } = null!;
        [JsonProperty("version")]
        public string Version { get; set; } = null!;
        [JsonProperty("releaseDate")]
        public string ReleaseDate { get; set; } = null!;
        [JsonProperty("checks")]
        public Dictionary<string, string> Checks { get; set; } = new Dictionary<string, string>();
    }
}
