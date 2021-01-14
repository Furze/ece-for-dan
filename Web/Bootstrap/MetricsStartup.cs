﻿using App.Metrics.Health;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.Web.Infrastructure.Extensions;
using MoE.ECE.Web.Infrastructure.Settings;

namespace MoE.ECE.Web.Bootstrap
{
    public class MetricsStartup : StartupConfig
    {
        public override void Configure(IApplicationBuilder app)
        {
            app.UsePingEndpoint();
            app.UseHealthAllEndpoints();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            AllowSynchronousCallsForAppMetricsBug(services);

            Configuration.BindFor<ConnectionStrings>();
            
            var health = AppMetricsHealth.CreateDefaultBuilder()
                .HealthChecks.RegisterFromAssembly(services) // configure options and add health checks
                .BuildAndAddTo(services);
            
            services.AddHealth(health);
            services.AddHealthEndpoints();
        }

        private static void AllowSynchronousCallsForAppMetricsBug(IServiceCollection services)
        {
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }
    }
}
