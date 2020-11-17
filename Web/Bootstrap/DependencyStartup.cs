using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using MoE.ECE.Domain.Infrastructure;
using MoE.ECE.Domain.Infrastructure.Abstractions;
using MoE.ECE.Domain.Infrastructure.Services.Opa;
using MoE.ECE.Domain.Integration;
using MoE.ECE.Domain.Services;
using MoE.ECE.Web.Infrastructure;
using MoE.ECE.Web.Infrastructure.Authorisation;
using MoE.ECE.Web.Infrastructure.Extensions;
using MoE.ECE.Web.Infrastructure.ServiceBus;
using MoE.ECE.Web.Infrastructure.Validation;
using Moe.Library.Cqrs;
using SystemClock = MoE.ECE.Domain.Infrastructure.SystemClock;

namespace MoE.ECE.Web.Bootstrap
{
    public class DependencyStartup : StartupConfig
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .ConfigureFor<OpaSettings>(Configuration);

            // Hosted Services

            // Singleton
            services.AddSingleton(Configuration);
            services.AddSingleton<IConnectionStringFactory, ConnectionStringFactory>();

            // Scoped

            services.AddScoped<IValidatorInterceptor, UseErrorCodeInterceptor>();
            services.AddScoped<IServiceBus, ServiceBusPublisher>();
            services.AddScoped<ServiceFactory>(ctx => ctx.GetService);
            services.AddScoped<ICqrs, MediatorCqrs>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehaviour<,>));

            services.AddScoped<ILoggedOnUser, LoggedOnUser>();
            services.AddScoped<ISystemClock, SystemClock>();

            services.AddAutoMapper(
                typeof(IAssemblyMarker),
                typeof(Domain.IAssemblyMarker));

            services.AddMediatR(typeof(Domain.IAssemblyMarker));

            services.Scan(scan => scan
                .FromAssembliesOf(
                    typeof(Domain.IAssemblyMarker), typeof(IAssemblyMarker))
                .AddClasses()
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddScoped<IOperationalFundingCalculator, OpaOperationalFundingCalculator>();
        }

        public override void Configure(IApplicationBuilder app)
        {
        }
    }
}