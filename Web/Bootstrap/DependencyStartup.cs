using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using MoE.ECE.Domain.Infrastructure;
using MoE.ECE.Domain.Infrastructure.Abstractions;
using MoE.ECE.Domain.Infrastructure.Services.Opa;
using MoE.ECE.Domain.Integration;
using MoE.ECE.Domain.Services;
using MoE.ECE.Integration.OpenPolicyAgent;
using MoE.ECE.Integration.OpenPolicyAgent.Services;
using MoE.ECE.Web.Infrastructure;
using MoE.ECE.Web.Infrastructure.Authorisation;
using MoE.ECE.Web.Infrastructure.Extensions;
using MoE.ECE.Web.Infrastructure.ServiceBus;
using MoE.ECE.Web.Infrastructure.Validation;
using Moe.Library.Cqrs;
using Moe.Library.ServiceBus;
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
            services.AddHostedService<WorkflowTopicConsumer>();
            services.AddHostedService<IntegrationTopicConsumer>();

            // Singleton
            services.AddSingleton(Configuration);
            services.AddSingleton<IConnectionStringFactory, ConnectionStringFactory>();
            services.AddSingleton<IServiceBus, ServiceBusPublisher>();
            services.AddSingleton(_ => new MessageFactory(typeof(Events.Integration.IAssemblyMarker)));

            // Scoped

            services.AddScoped<IValidatorInterceptor, UseErrorCodeInterceptor>();
            services.AddScoped<ServiceFactory>(ctx => ctx.GetService);
            services.AddScoped<ICqrs, MediatorCqrs>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehaviour<,>));

            services.AddScoped<ILoggedOnUser, LoggedOnUser>();
            services.AddScoped<ISystemClock, SystemClock>();

            services.AddAutoMapper(
                typeof(IAssemblyMarker),
                typeof(Domain.IAssemblyMarker));

            services.AddMediatR(typeof(Domain.IAssemblyMarker));

            services.AddScoped<IOperationalFundingCalculator, OpaOperationalFundingCalculator>();
            
            // Authorisation
            services.AddSingleton<IAuthorizationHandler, RequiredPermissionAuthorisationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, RequiredPermissionPolicyProvider>();
            
            if (LocalDevelopment.DisableAuthnAndAuthz(Environment))
            {
                services.AddSingleton<IOpenPolicyAgentService, FakeOpenPolicyAgentService>();
            }
            else
            {
                services.AddOpenPolicyAgentHttpClient();
                services.AddSingleton<IOpenPolicyAgentService, OpenPolicyAgentService>();
            }
        }

        public override void Configure(IApplicationBuilder app)
        {
        }
    }
}