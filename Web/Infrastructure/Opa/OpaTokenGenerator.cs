using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MoE.ECE.Domain.Services.Opa;
using Newtonsoft.Json;

namespace MoE.ECE.Web.Infrastructure.Opa
{
    public class OpaTokenGenerator : IOpaTokenGenerator
    {
        private readonly OpaSettings _settings;
        private readonly IHttpClientFactory _clientFactory;

        public OpaTokenGenerator(IHttpClientFactory clientFactory, IOptions<OpaSettings> settings)
        {
            _settings = AssertSettings(settings);
            _clientFactory = clientFactory;
        }

        private OpaSettings AssertSettings(IOptions<OpaSettings> options)
        {
            var opaSettings = options.Value;

            return opaSettings;
        }

        public async Task<OpaAccessToken?> GenerateAsync()
        {
            var requestData = new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = _settings.UserName,
                ["client_secret"] = _settings.UserSecret
            };

            var client = _clientFactory.CreateClient();

            var request = new FormUrlEncodedContent(requestData);
            
            var response = await client.PostAsync(_settings.AuthorisationUrl, request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OpaAccessToken>(responseContent);
            }

            return null;
        }
    }
}