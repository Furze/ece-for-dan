using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.Web.Infrastructure.Middleware.Exceptions;

namespace MoE.ECE.Web.Bootstrap
{
    public class ExceptionStartup : StartupConfig
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }

        public override void Configure(IApplicationBuilder app) =>
            app
                .UseMiddleware<UnhandledExceptionMiddleware>()
                .UseMiddleware<ResourceNotFoundExceptionMiddleware>()
                .UseMiddleware<BadDataExceptionMiddleware>()
                .UseMiddleware<BadRequestExceptionMiddleware>()
                .UseMiddleware<ValidationExceptionMiddleware>();
    }
}