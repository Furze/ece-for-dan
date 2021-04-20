using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MoE.ECE.Integration.OpenPolicyAgent.Model
{
    public class OpenPolicyAgentInput
    {
        public OpenPolicyAgentInput(HttpRequest httpContextRequest, string apiName, List<string> requestedPermissions)
        {
            Api = apiName;
            Jwt = GetJwt(httpContextRequest.Headers["Authorization"]);
            RequestMethod = httpContextRequest.Method;
            RequestPath = httpContextRequest.Path.Value?.Split("/")
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList() ?? new List<string>();

            RequestedPermissions = requestedPermissions;
        }

        [JsonProperty("api")]
        public string Api { get; set; }

        [JsonProperty("jwt")]
        public string? Jwt { get; set; }

        [JsonProperty("http_method")]
        public string RequestMethod { get; set; }

        [JsonProperty("request_path")]
        public List<string> RequestPath { get; set; }

        [JsonProperty("requested_permissions")]
        public List<string> RequestedPermissions { get; set; }

        private static string GetJwt(IEnumerable<string> authHeader)
        {
            var parts = Regex.Split(authHeader.FirstOrDefault() ?? "", "bearer", RegexOptions.IgnoreCase);
            return parts.Length == 2 ? parts[1].Trim() : "";
        }
    }
}