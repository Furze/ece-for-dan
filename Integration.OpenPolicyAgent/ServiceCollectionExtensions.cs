using System;
using System.Net.Http.Headers;
using System.Net.Mime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MoE.ECE.Integration.OpenPolicyAgent
{
    public static class ServiceCollectionExtensions
    {
        private static string? _bearerToken;
        
        public static void AddOpenPolicyAgentHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("open-policy-agent", (provider, httpClient) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                if (configuration == null)
                    throw new InvalidOperationException("OpenPolicyAgent:null value set in configuration. ");

                EnsureOpenPolicyAgentCredentials(configuration);

                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

                httpClient
                    .DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
            });
        }

        private static void EnsureOpenPolicyAgentCredentials(IConfiguration configuration)
        {
            _bearerToken = configuration["OpenPolicyAgent:BearerToken"];

            if (string.IsNullOrEmpty(_bearerToken))
                throw new InvalidOperationException("OpenPolicyAgent:BearerToken is not set in configuration. ");
        }

    }
}