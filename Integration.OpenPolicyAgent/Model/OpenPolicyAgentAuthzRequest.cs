using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MoE.ECE.Integration.OpenPolicyAgent.Model
{
    public class OpenPolicyAgentAuthzRequest
    {
        public OpenPolicyAgentAuthzRequest(HttpRequest httpContextRequest, string apiName, List<string> requestedPermissions)
        {
            Input = new OpenPolicyAgentInput(httpContextRequest, apiName, requestedPermissions);
        }

        [JsonProperty("input")]
        public OpenPolicyAgentInput Input { get; set; }

    }
}