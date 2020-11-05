using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MoE.ECE.Web.Bootstrap
{
    /// <summary>
    ///     Base implementation for the configuration of related service dependencies and middleware
    ///     request pipelines.
    /// </summary>
    public abstract class StartupConfig
    {
        private IConfiguration? _configuration;

        public IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                    throw new InvalidOperationException($"{nameof(Configuration)} not set.");
                return _configuration;
            }
            set => _configuration = value;
        }

        public IWebHostEnvironment? Environment { get; set; }

        public abstract void ConfigureServices(IServiceCollection services);

        public abstract void Configure(IApplicationBuilder app);
    }
}