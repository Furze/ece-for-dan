using Newtonsoft.Json;

namespace MoE.ECE.Domain.Infrastructure.Services.Opa
{
    public class OpaError
    {
        [JsonProperty(PropertyName = "@id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "entity")]
        public string? Entity { get; set; }

        [JsonProperty(PropertyName = "detail")]
        public string? Detail { get; set; }
    }
}
