using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MoE.ECE.Domain.Infrastructure.Services.Opa;
using Newtonsoft.Json;

namespace MoE.ECE.Web.Infrastructure.Opa
{
    public class OpaTokenGenerator : IOpaTokenGenerator
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly OpaSettings _settings;

        public OpaTokenGenerator(IHttpClientFactory clientFactory, IOptions<OpaSettings> settings)
        {
            _settings = AssertSettings(settings);
            _clientFactory = clientFactory;
        }

        public async Task<OpaAccessToken?> GenerateAsync()
        {
            Dictionary<string, string>? requestData = new
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = _settings.UserName,
                ["client_secret"] = _settings.UserSecret
            };

            HttpClient? client = _clientFactory.CreateClient();

            FormUrlEncodedContent? request = new(requestData);

            HttpResponseMessage? response = await client.PostAsync(_settings.AuthorisationUrl, request);

            if (response.IsSuccessStatusCode)
            {
                string? responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OpaAccessToken>(responseContent);
            }

            return null;
        }

        private OpaSettings AssertSettings(IOptions<OpaSettings> options)
        {
            OpaSettings? opaSettings = options.Value;

            return opaSettings;
        }
    }
}