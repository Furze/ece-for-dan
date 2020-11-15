using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MoE.ECE.Domain.Exceptions;

namespace MoE.ECE.Web.Infrastructure.Middleware.Exceptions
{
    public class ResourceNotFoundExceptionMiddleware : ExceptionMiddlewareBase<ResourceNotFoundException>
    {
        public ResourceNotFoundExceptionMiddleware(
            RequestDelegate next,
            ILogger<BadRequestExceptionMiddleware> logger)
            : base(next, logger)
        {
        }

        protected override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        protected override ErrorResponse? CreateResponse(HttpContext context, ResourceNotFoundException exception) =>
            null;
    }
}