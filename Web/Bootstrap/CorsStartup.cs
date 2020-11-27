using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
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
                        ConfigureExposedHeaders(settings, builder);

                        ConfigureAllowedHeaders(settings, builder);

                        ConfigureAllowedMethods(settings, builder);

                        ConfigureAllowedOrigins(settings, builder);

                        ConfigureAllowedCredentials(settings, builder);
                    });
            });
        }

        public override void Configure(IApplicationBuilder app) => app.UseCors("default");
        
        private static void ConfigureExposedHeaders(CorsSettings settings, CorsPolicyBuilder builder)
        {
            if (settings.ExposedHeaders != null)
            {
                builder.WithExposedHeaders(settings.ExposedHeaders);
            }
        }

        private static void ConfigureAllowedCredentials(CorsSettings settings, CorsPolicyBuilder builder)
        {
            if (settings.AllowCredentials)
            {
                builder.AllowCredentials();
            }
        }

        private static void ConfigureAllowedOrigins(CorsSettings settings, CorsPolicyBuilder builder)
        {
            if (settings.AllowedOrigins == null)
            {
                return;
            }

            if (settings.AllowedOrigins.Contains("*"))
            {
                builder.AllowAnyOrigin();
            }
            else
            {
                builder.WithOrigins(settings.AllowedOrigins);
            }
        }

        private static void ConfigureAllowedMethods(CorsSettings settings, CorsPolicyBuilder builder)
        {
            if (settings.AllowedMethods == null)
            {
                return;
            }

            if (settings.AllowedMethods.Contains("*"))
            {
                builder.AllowAnyMethod();
            }
            else
            {
                builder.WithMethods(settings.AllowedMethods);
            }
        }

        private static void ConfigureAllowedHeaders(CorsSettings settings, CorsPolicyBuilder builder)
        {
            if (settings.AllowedHeaders == null)
            {
                return;
            }

            if (settings.AllowedHeaders.Contains("*"))
            {
                builder.AllowAnyHeader();
            }
            else
            {
                builder.WithHeaders(settings.AllowedHeaders);
            }
        }
    }
}