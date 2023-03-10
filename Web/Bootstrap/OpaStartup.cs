using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.Domain.Infrastructure.Services.Opa;
using MoE.ECE.Web.Infrastructure.Extensions;
using MoE.ECE.Web.Infrastructure.Opa;

namespace MoE.ECE.Web.Bootstrap
{
    public class OpaStartup : StartupConfig
    {
        public override void Configure(IApplicationBuilder app)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .ConfigureFor<OpaSettings>(Configuration);

            services.AddHttpClient<IOpaClient, OpaClient>();
            services.AddHttpClient();
            services.AddSingleton<IOpaTokenGenerator, OpaTokenGenerator>();
        }
    }
}