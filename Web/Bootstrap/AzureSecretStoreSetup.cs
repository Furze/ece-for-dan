using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Rest.TransientFaultHandling;

namespace MoE.ECE.Web.Bootstrap
{
    /// <summary>
    ///     Retrieves application secrets from the Azure keyvault.
    /// </summary>
    public class AzureSecretStoreSetup : ISecretStoreSetup
    {
        public void Configure(IConfigurationRoot configuration, IConfigurationBuilder builder,
            WebHostBuilderContext context)
        {
            if (context.HostingEnvironment.IsDevelopment() || context.HostingEnvironment.IsEnvironment("E2E"))
            {
                return;
            }

            string? keyVaultName = configuration["KeyVault:Vault"];

            AzureServiceTokenProvider? tokenProvider = new(
                context.HostingEnvironment.IsDevelopment()
                    ? "RunAs=Developer; DeveloperTool=VisualStudio;"
                    : "RunAs=App;");
            KeyVaultClient? keyVaultClient = new((authority, resource, scope) =>
                tokenProvider.KeyVaultTokenCallback(authority, resource, scope));
            keyVaultClient.SetRetryPolicy(new RetryPolicy<HttpStatusCodeErrorDetectionStrategy>(
                new ExponentialBackoffRetryStrategy(
                    5,
                    TimeSpan.FromSeconds(1.0),
                    TimeSpan.FromSeconds(16.0),
                    TimeSpan.FromSeconds(2.0))));
            // Clear the sources so we can slip the key vault provider in first.
            builder.Sources.Clear();
            builder.AddAzureKeyVault(
                $"https://{keyVaultName}.vault.azure.net/",
                keyVaultClient,
                new DefaultKeyVaultSecretManager()
            );
        }
    }
}