using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MoE.ECE.Web.Bootstrap;
using MoE.ECE.Web.Infrastructure.Middleware.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;

namespace MoE.ECE.Web.Infrastructure.Extensions
{
    public static class WebHostBuilderExtensions
    {
        /// <summary>
        /// Applies the configuration lookup key/value pairs using the following source order:
        /// - Azure Key Vault
        /// - Environment Variables
        /// - appsettings.json
        /// - appsettings.[env].json.
        /// </summary>
        public static IWebHostBuilder UseConfiguration(this IWebHostBuilder hostBuilder, ISecretStoreSetup secretsSetup)
        {
            hostBuilder.ConfigureWebHost();
            hostBuilder.ConfigureApplication(secretsSetup);

            return hostBuilder;
        }

        /// <summary>
        /// Applies the configuration required by the web host to use.
        /// </summary>
        private static void ConfigureWebHost(this IWebHostBuilder hostBuilder)
        {
            // First we setup the configuration of the WebHostBuilder.
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true);

            var config = configBuilder.Build();

            hostBuilder.UseConfiguration(config);
        }

        /// <summary>
        /// Applies the configuration for the application to use.
        /// </summary>
        private static void ConfigureApplication(this IWebHostBuilder hostBuilder, ISecretStoreSetup secretsSetup)
        {
            // Now we setup the configuration used in the application services.
            hostBuilder.ConfigureAppConfiguration((context, builder) =>
            {
                // Save the current config sources, because we're going to do some reordering.
                var originalSources = builder.Sources.ToList();

                // We're building as is currently configured as a interim measure to get the current
                // key vault settings.
                var configuration = builder.Build();

                // Clear the sources so we can slip the key vault provider in first.
                builder.Sources.Clear();
                secretsSetup.Configure(configuration, builder, context);

                // Finally, reapply the original config sources.
                originalSources.ForEach(builder.Sources.Add);
            });
        }

        /// <summary>
        /// Configures Serilog to be read from the configuration file and enriched
        /// from the context.
        /// </summary>
        public static IWebHostBuilder ConfigureLogging(this IWebHostBuilder hostBuilder)
        {
            return hostBuilder
                .UseSerilog((hostingContext, loggerConfiguration) =>
                    loggerConfiguration
                        .Filter.ByExcluding(le => le.Exception is TaskCanceledException)
                        .Enrich.FromLogContext()
                        .Enrich.With<VersionTagEnricher>()
                        .Enrich.WithProperty("API", "Schools")
                        .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                            .WithDefaultDestructurers()
                            .WithDestructurers(new[] {new DbUpdateExceptionDestructurer()}))
                        .WriteTo
                            .ApplicationInsights(TelemetryConfiguration.CreateDefault(),
                                                 TelemetryConverter.Traces,
                                                 LogEventLevel.Debug)
                        .ReadFrom.Configuration(hostingContext.Configuration));
        }
    }
}