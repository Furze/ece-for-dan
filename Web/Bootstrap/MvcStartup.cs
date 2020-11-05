using System.Text.Json.Serialization;
using Hangfire;
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
                    options.RegisterValidatorsFromAssemblyContaining<MoE.ECE.Domain.IAssemblyMarker>();
                })
                .AddApplicationPart(typeof(IAssemblyMarker).Assembly);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public override void Configure(IApplicationBuilder app)
        {
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            var dashboardOptions = new DashboardOptions();
            if (!Environment.IsDevelopment())
                dashboardOptions.Authorization = new[] {new HangfireAuthorizationFilter()};
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                if (Environment.IsDevelopment())
                    endpoints.MapHangfireDashboard();
                else
                    endpoints.MapHangfireDashboard(dashboardOptions).RequireAuthorization("Hangfire");
            });
        }
    }
}