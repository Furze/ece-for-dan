using System.Text.Json.Serialization;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoE.ECE.Web.Infrastructure.Authorisation;
using MoE.ECE.Web.Infrastructure.Validation;

namespace MoE.ECE.Web.Bootstrap
{
    public class MvcStartup : StartupConfig
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddCodedFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<IAssemblyMarker>();
                    options.RegisterValidatorsFromAssemblyContaining<Domain.IAssemblyMarker>();
                })
                .AddApplicationPart(typeof(IAssemblyMarker).Assembly);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                var hangfireDashboard = endpoints.MapHangfireDashboard(new DashboardOptions
                {
                    Authorization = new[] { new HangfireAuthorizationFilter() }
                });
                if (Environment.IsDevelopment())
                {
                    hangfireDashboard.Add(endpointBuilder =>
                    {
                        endpointBuilder.Metadata.Add(new AllowAnonymousAttribute());
                    });
                }
                else
                {
                    hangfireDashboard.RequireAuthorization(RequiredPermissionPolicyProvider.HangfirePolicy);
                }
            });
        }
        
        private class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                // This filter is needed to get auth to work.
                // The permissions are handled in RequiredPermissionPolicyProvider.
                return true;
            }
        }
    }
}