using System.Reflection;
using System.Threading.Tasks;
using Events.Integration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MoE.ECE.Web.Infrastructure.HealthChecks;
using Newtonsoft.Json;
using Moe.Library.ServiceBus;

namespace MoE.ECE.Web.Bootstrap
{
    public class HealthCheckStartup : StartupConfig
    {
        private const string ServiceBusConnectionStringMarker = "ConnectionStrings:ServiceBus";
        private const string ReleaseDateMarker = "ReleaseDate";

        public override void Configure(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = WriteResponse
                });
                endpoints.MapGet("/ping", async context =>
                {
                    await context.Response.WriteAsync("pong");
                });
            });
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<PostgresHealthCheck>(PostgresHealthCheck.HealthCheckName)
                .AddAzureServiceBusTopicConnectivityCheck(Configuration[ServiceBusConnectionStringMarker], Constants.Topic.ECE);
        }

        private Task WriteResponse(HttpContext context, HealthReport result)
        {
            var versionNumber = Assembly.GetEntryAssembly()?.GetName().Version?.ToString();
            versionNumber = string.IsNullOrWhiteSpace(versionNumber)
                ? "Local build"
                : versionNumber;

            var releaseDateValue = Configuration[ReleaseDateMarker];
            releaseDateValue = string.IsNullOrWhiteSpace(releaseDateValue)
                ? "Local build"
                : releaseDateValue;

            var response = new Response
            {
                Status = result.Status.ToString(),
                Name = "ECE API",
                Version = versionNumber,
                ReleaseDate = releaseDateValue
            };

            foreach (var (key, value) in result.Entries)
            {
                var status = value.Status.ToString();
                if (value.Status != HealthStatus.Healthy && !string.IsNullOrEmpty(value.Description))
                {
                    status += $" ({value.Description})";
                }

                response.Checks.Add(key, status);
            }

            context.Response.ContentType = "application/json; charset=utf-8";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response, Formatting.Indented));
        }
    }
}
