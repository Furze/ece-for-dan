using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MoE.ECE.Web.Bootstrap
{
    public class StartupConfigRegistry
    {
        private readonly IConfiguration _configuration;
        private readonly List<StartupConfig> _configurations = new();
        private readonly IWebHostEnvironment _environment;

        public StartupConfigRegistry(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public StartupConfigRegistry Register<TStartup>()
            where TStartup : StartupConfig, new()
        {
            TStartup? startup = new {Configuration = _configuration, Environment = _environment};

            _configurations.Add(startup);

            return this;
        }

        public void ConfigureServices(IServiceCollection services) =>
            _configurations.ForEach(startup => startup.ConfigureServices(services));

        public void Configure(IApplicationBuilder app) => _configurations.ForEach(startup => startup.Configure(app));
    }
}