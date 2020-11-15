using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace MoE.ECE.Web.Bootstrap
{
    public class Startup : EceApiStartup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
            : base(configuration, environment)
        {
        }

        protected override StartupConfigRegistry CreateRegistry(IConfiguration configuration,
            IWebHostEnvironment environment) =>
            new StartupConfigRegistry(configuration, environment)
                .Register<AppSettingsStartup>()
                .Register<AuthenticationStartup>()
                .Register<AuthorisationStartup>()
                .Register<LoggingStartup>()
                .Register<DependencyStartup>()
                .Register<OpaStartup>()
                .Register<MartenStartup>()
                .Register<EntityFrameworkStartup>()
                .Register<HangfireStartup>()
                .Register<ExceptionStartup>()
                .Register<CorsStartup>()
                .Register<SwaggerStartup>()
                .Register<MetricsStartup>()
                .Register<MvcStartup>();
    }
}