using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.Web.Infrastructure.Extensions;
using MoE.ECE.Web.Infrastructure.Settings;

namespace MoE.ECE.Web.Bootstrap
{
    public class CorsStartup : StartupConfig
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            var settings = Configuration.BindFor<CorsSettings>();
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "default",
                    builder =>
                    {
                        if (settings.ExposedHeaders != null) builder.WithExposedHeaders(settings.ExposedHeaders);

                        if (settings.AllowedHeaders != null)
                        {
                            if (settings.AllowedHeaders.Contains("*"))
                                builder.AllowAnyHeader();
                            else
                                builder.WithHeaders(settings.AllowedHeaders);
                        }

                        if (settings.AllowedMethods != null)
                        {
                            if (settings.AllowedMethods.Contains("*"))
                                builder.AllowAnyMethod();
                            else
                                builder.WithMethods(settings.AllowedMethods);
                        }

                        if (settings.AllowedOrigins != null)
                        {
                            if (settings.AllowedOrigins.Contains("*"))
                                builder.AllowAnyOrigin();
                            else
                                builder.WithOrigins(settings.AllowedOrigins);
                        }

                        if (settings.AllowCredentials) builder.AllowCredentials();
                    });
            });
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UseCors("default");
        }
    }
}