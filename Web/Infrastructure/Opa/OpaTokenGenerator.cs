using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MoE.ECE.Domain.Infrastructure.Services.Opa;
using Newtonsoft.Json;

namespace MoE.ECE.Web.Infrastructure.Opa
{
    public class OpaTokenGenerator : IOpaTokenGenerator
    {
        private const string OpaAccessTokenCacheKey = "OPA_ACCESS_TOKEN";
        private readonly OpaSettings _settings;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMemoryCache _cache;

        public OpaTokenGenerator(IHttpClientFactory clientFactory, IOptions<OpaSettings> settings, IMemoryCache cache)
        {
            _settings = AssertSettings(settings);
            _clientFactory = clientFactory;
            _cache = cache;
        }

        private OpaSettings AssertSettings(IOptions<OpaSettings> options)
        {
            var opaSettings = options.Value;

            return opaSettings;
        }

        public async Task<OpaAccessToken?> GetTokenAsync()
        {
            if (!_cache.TryGetValue(OpaAccessTokenCacheKey, out OpaAccessToken? token))
            {
                token = await GenerateAsync();
                if (token == null)
                {
                    return null;
                }

                // set the cache expiry a little shorter than what the token says to avoid knife edge timing issues.
                // Since we don't have control of the value of ExpiresIn (typically 1800) we shorten by a
                // percentage. e.g. 93% of 1800s = 1674s. This is just over a 2min buffer which should be plenty enough.
                var expiryInSeconds = Math.Round(token.ExpiresIn * 0.93d);
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(expiryInSeconds));

                _cache.Set(OpaAccessTokenCacheKey, token, cacheOptions);
            }

            return token;
        }
        
        private async Task<OpaAccessToken?> GenerateAsync()
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