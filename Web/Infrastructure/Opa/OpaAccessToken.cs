using Newtonsoft.Json;

namespace MoE.ECE.Web.Infrastructure.Opa
{
    public class OpaAccessToken
    {
        [JsonProperty("Access_token")] public string TokenValue { get; set; } = string.Empty;

        [JsonProperty("Token_type")] public string TokenType { get; set; } = string.Empty;

        [JsonProperty("Expires_in")] public long ExpiresIn { get; set; }
    }
}