using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MoE.ECE.Web.Infrastructure.Authorisation;
using MoE.ECE.Web.Infrastructure.Extensions;
using MoE.ECE.Web.Infrastructure.Settings;
using Serilog;

namespace MoE.ECE.Web.Bootstrap
{
    public class LoggingStartup : StartupConfig
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // The recommended approach is to configure Serilog in the program.            
            // AppInsights will get instrumentation key from env var: APPINSIGHTS_INSTRUMENTATIONKEY
            services.AddApplicationInsightsTelemetry();
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.ApplicationServices
                .GetService<ILoggerFactory>()
                .AddSerilog();
        }
    }

    public class AuthenticationStartup : StartupConfig
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            var settings = Configuration.BindFor<OidcSettings>();

            if (Environment.IsDevelopment())
            {
                // Disable authentication and authorization.
                services.TryAddSingleton<IPolicyEvaluator, DisableAuthenticationPolicyEvaluator>();
            }

            services
                .AddAuthentication(cfg =>
                {
                    cfg.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    cfg.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(options =>
                {
                    options.Authority = settings.Authority;

                    options.ClientId = "schools-api";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";
                    options.UsePkce = true;

                    options.SaveTokens = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.FromMinutes(5),
                        RequireSignedTokens = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidIssuer = settings.Issuer,
                        ValidateIssuerSigningKey = true,
                    };
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = settings.Authority;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Clock skew compensates for server time drift.
                        ClockSkew = TimeSpan.FromMinutes(5),
                        // Specify the key used to sign the token:
                        //IssuerSigningKeys = signingKeys,
                        RequireSignedTokens = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidAudience = settings.Audience,
                        ValidateIssuer = true,
                        ValidIssuer = settings.Issuer
                    };
                });
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}