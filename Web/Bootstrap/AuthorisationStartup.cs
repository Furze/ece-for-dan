using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MoE.ECE.Web.Bootstrap
{
    public class AuthorisationStartup : StartupConfig
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // The default policy is to at least insist the user is authenticated
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });
        }

        public override void Configure(IApplicationBuilder app)
        {
        }
    }
}