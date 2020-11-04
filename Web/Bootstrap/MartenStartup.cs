using System;
using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.Domain.Infrastructure;
using MoE.ECE.Web.Infrastructure.Extensions;

namespace MoE.ECE.Web.Bootstrap
{
    public class MartenStartup : StartupConfig
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(ConfigureMarten);

            // Scoped
            services.AddScoped(provider =>
                provider.GetService<IDocumentStore>().LightweightSession());
        }

        public override void Configure(IApplicationBuilder app)
        {
        }

        private IDocumentStore ConfigureMarten(IServiceProvider serviceProvider)
        {
            var settings = (Configuration ?? throw new NullReferenceException($"{nameof(Configuration)} not set"))
                .BindFor<MartenSettings>();

            var connectionStringFactory = serviceProvider.GetService<IConnectionStringFactory>();
            settings.ConnectionString = connectionStringFactory.GetConnectionString();

            return DocumentStore.For(storeOptions =>
                ECEMartenRegistry.ApplyDefaultConfiguration(storeOptions, settings, serviceProvider));
        }
    }
}