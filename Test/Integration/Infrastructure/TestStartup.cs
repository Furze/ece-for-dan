using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MoE.ECE.Web.Bootstrap;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    public class TestStartup : EceApiStartup
    {
        public TestStartup(IConfiguration configuration, IWebHostEnvironment environment)
            : base(configuration, environment)
        {
        }

        protected override StartupConfigRegistry CreateRegistry(IConfiguration configuration,
            IWebHostEnvironment environment) =>
            new StartupConfigRegistry(configuration, environment)
                .Register<AppSettingsStartup>()
                .Register<AuthenticationStartup>()
                .Register<LoggingStartup>()
                .Register<DependencyStartup>()
                .Register<OpaStartup>()
                .Register<MartenStartup>()
                .Register<EntityFrameworkStartup>()
                .Register<HangfireStartup>()
                .Register<ExceptionStartup>()
                .Register<CorsStartup>()
                .Register<SwaggerStartup>()
                .Register<MvcStartup>();
    }
}