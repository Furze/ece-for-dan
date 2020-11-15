using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.Domain.Infrastructure;
using MoE.ECE.Domain.Infrastructure.EntityFramework;

namespace MoE.ECE.Web.Bootstrap
{
    public class EntityFrameworkStartup : StartupConfig
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // Entity configurations
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(Domain.IAssemblyMarker))
                .AddClasses(filter => filter.AssignableToAny(typeof(IEntityConfiguration)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

            // Setup the context to use Postgres
            ConnectionStringFactory? connectionStringFactory = new(Configuration);
            services.AddDbContext<ReferenceDataContext>(options =>
            {
                options
                    .UseNpgsql(connectionStringFactory.GetConnectionString())
                    .UseSnakeCaseNamingConvention()
                    .UseQueryTrackingBehavior(QueryTrackingBehavior
                        .NoTracking); // Reference data doesn't need to be tracked
            });
        }

        public override void Configure(IApplicationBuilder app)
        {
        }
    }
}