using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MoE.ECE.Web.Infrastructure.Middleware.Exceptions
{
    /// <summary>
    /// Base implementation for handling specific type of exceptions thrown by the application
    /// and converting to an appropriate response to the caller.
    /// </summary>
    /// <typeparam name="T">The generic type of the exception to handle.</typeparam>
    public abstract class ExceptionMiddlewareBase<T>
        where T : Exception
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        protected ExceptionMiddlewareBase(
            RequestDelegate next,
            ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// The expected http status that this type of exception should be returned as.
        /// </summary>
        protected abstract HttpStatusCode StatusCode { get; }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (T ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Creates the message that will be given to the caller as a reason for the failure.
        /// </summary>
        protected virtual string CreateMessage(T exception) => exception.Message;

        protected virtual void LogException(T exception)
        {
            _logger.LogError(exception, CreateMessage(exception));
        }

        protected virtual ErrorResponse? CreateResponse(HttpContext context, T exception)
        {
            return new ErrorResponse
            {
                Errors = new[]
                {
                    new Error
                    {
                        Message = CreateMessage(exception)
                    }
                }
            };
        }

        /// <summary>
        /// Handles the given exception by ensuring that the correct response is returned to the
        /// caller and it is logged.
        /// </summary>
        protected Task HandleExceptionAsync(HttpContext context, T exception)
        {
            context.Response.StatusCode = (int) StatusCode;
            context.Response.ContentType = "application/json";

            var response = CreateResponse(context, exception);
            var result = response == null ? "" : JsonConvert.SerializeObject(response, GetSerializerSettings());

            LogException(exception);

            return context.Response.WriteAsync(result);
        }

        private static JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
    }
}