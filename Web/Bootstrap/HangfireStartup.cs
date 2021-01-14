using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoE.ECE.Domain.Infrastructure;
using MoE.ECE.Web.Infrastructure.Extensions;
using MoE.ECE.Web.Infrastructure.Settings;

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
            services.AddHangfireServer(options =>
            {
                // When developing locally only want one worker
                options.WorkerCount = Environment.IsDevelopment()
                    ? 1
                    : Configuration.BindFor<HangfireSettings>().WorkerCount;
            });
        }

        public override void Configure(IApplicationBuilder app)
        {
        }
    }
}