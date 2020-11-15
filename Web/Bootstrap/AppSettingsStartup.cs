using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MoE.ECE.Web.Infrastructure.Extensions;
using MoE.ECE.Web.Infrastructure.Settings;

namespace MoE.ECE.Web.Bootstrap
{
    /// <summary>
    ///     Configures any configuration settings that should be made available for injecting as
    ///     an <see cref="IOptions{TOptions}" /> implementation.
    /// </summary>
    public class AppSettingsStartup : StartupConfig
    {
        public override void ConfigureServices(IServiceCollection services) =>
            services
                .ConfigureFor<OidcSettings>(Configuration)
                .ConfigureFor<ConnectionStrings>(Configuration);

        public override void Configure(IApplicationBuilder app)
        {
        }
    }
}