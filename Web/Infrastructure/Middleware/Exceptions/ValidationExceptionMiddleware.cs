using System.Linq;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MoE.ECE.Web.Infrastructure.Middleware.Exceptions
{
    public class ValidationExceptionMiddleware : ExceptionMiddlewareBase<ValidationException>
    {
        public ValidationExceptionMiddleware(
            RequestDelegate next,
            ILogger<BadRequestExceptionMiddleware> logger)
            : base(next, logger)
        {
        }

        protected override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        protected override ErrorResponse CreateResponse(HttpContext context, ValidationException exception)
        {
            return new ErrorResponse
            {
                Errors = exception.Errors.Select(error =>
                    new Error
                    {
                        ErrorCode = error.ErrorCode,
                        Property = error.PropertyName,
                        Message = error.ErrorMessage
                    })
            };
        }
    }
}