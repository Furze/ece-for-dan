using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoE.ECE.Integration.OpenPolicyAgent.Model;
using Newtonsoft.Json;

namespace MoE.ECE.Integration.OpenPolicyAgent.Services
{
    public class OpenPolicyAgentService : IOpenPolicyAgentService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OpenPolicyAgentService> _logger;

        public OpenPolicyAgentService(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ILogger<OpenPolicyAgentService> logger)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<bool> IsPermittedAsync(string requiredPermission, HttpRequest httpRequest)
        {
            var requestUri = CombineUriAndPath(
                _configuration["OpenPolicyAgent:BaseUri"],
                _configuration["OpenPolicyAgent:AuthZPath"]);

            var authRequest = new OpenPolicyAgentAuthzRequest(httpRequest, "schools-api", new List<string> { requiredPermission });
            try
            {
                var authZResponse = await CallOpenPolicyAgentAsync<OpenPolicyAgentAuthzRequest, OpenPolicyAgentAuthzResponse>(authRequest, requestUri);
                return authZResponse.Allow;
            }
            catch (Exception ex)
            {
                _logger.LogCritical("OpenPolicyAgent request {@request} to {path} failed with exception {@exception}. Not permitting user's request. ",
                    authRequest,
                    requestUri,
                    ex);

                return false;
            }
        }

        private static string CombineUriAndPath(string uri, string path)
            => $"{uri.TrimEnd('/')}/{path.TrimStart('/')}";

        private async Task<TResponse> CallOpenPolicyAgentAsync<TRequest, TResponse>(TRequest request, string requestUri)
            where TResponse : new()
        {
            var serialisedOpenPolicyAgentInput = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            _logger.LogDebug("POSTing to {requestUri} value '{input}'",
                requestUri,
                serialisedOpenPolicyAgentInput);

            var httpClient = _httpClientFactory.CreateClient("open-policy-agent");
            using var httpResponse = await httpClient.PostAsync(requestUri,
                new StringContent(serialisedOpenPolicyAgentInput, Encoding.UTF8, "application/json"));

            var rawOpenPolicyAgentResponse = await httpResponse.Content.ReadAsStringAsync();

            _logger.LogDebug("Received {code} from OpenPolicyAgent '{@rawOpenPolicyAgentResponse}'",
                httpResponse.StatusCode,
                rawOpenPolicyAgentResponse);

            httpResponse.EnsureSuccessStatusCode();

            var openPolicyAgentResponseResponse = JsonConvert.DeserializeObject<OpenPolicyAgentResult<TResponse>>(rawOpenPolicyAgentResponse);

            _logger.LogDebug("Deserialisation result: {@openPolicyAgentResponseResponse}", openPolicyAgentResponseResponse);

            return openPolicyAgentResponseResponse.Result;
        }
    }
}