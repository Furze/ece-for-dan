using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.Domain.Abstractions;
using MoE.ECE.Domain.Infrastructure;
using MoE.ECE.Domain.Integration;
using MoE.ECE.Web.Infrastructure;
using MoE.ECE.Web.Infrastructure.Authorisation;
using MoE.ECE.Web.Infrastructure.ServiceBus;
using MoE.ECE.Web.Infrastructure.Validation;
using Moe.Library.Cqrs;

namespace MoE.ECE.Web.Bootstrap
{
    public class DependencyStartup : StartupConfig
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // Hosted Services
            //services.AddHostedService<FirstWorkflowConsumer>();
            
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
            services.AddScoped<Microsoft.Extensions.Internal.ISystemClock, SystemClock>();

            
            services.AddAutoMapper(
                typeof(IAssemblyMarker),
                typeof(MoE.ECE.Domain.IAssemblyMarker));
            
            services.AddMediatR(typeof(Domain.IAssemblyMarker));
        }

        public override void Configure(IApplicationBuilder app)
        {
        }
    }
}