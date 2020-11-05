using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MoE.ECE.Web.Bootstrap
{
    public abstract class ECEApiStartup
    {
        private readonly StartupConfigRegistry _registry;

        /// <summary>
        ///     The order of registration is important from the middleware pipeline configuration
        ///     aspect. <see cref="StartupConfig.Configure(IApplicationBuilder)" />.
        /// </summary>
        protected ECEApiStartup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            _registry = CreateRegistry(configuration, environment);
        }

        /// <summary>
        ///     This method gets called by the runtime.
        /// </summary>
        /// <param name="services">The container to add any services to.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            _registry.ConfigureServices(services);
        }

        /// <summary>
        ///     This method gets called by the runtime.
        /// </summary>
        /// <remarks>
        ///     Use this method to configure the HTTP request pipeline.
        /// </remarks>
        public void Configure(IApplicationBuilder app)
        {
            _registry.Configure(app);
        }

        protected abstract StartupConfigRegistry CreateRegistry(
            IConfiguration configuration,
            IWebHostEnvironment environment);
    }
}