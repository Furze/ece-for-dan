using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.Domain.Infrastructure;

namespace MoE.ECE.Web.Bootstrap
{
    public class HangfireStartup : StartupConfig
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            var connectionStringFactory = new ConnectionStringFactory(Configuration);

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(connectionStringFactory.GetConnectionString())
            );

            // Add the processing server as IHostedService
            services.AddHangfireServer();
        }

        public override void Configure(IApplicationBuilder app)
        {
        }
    }
}