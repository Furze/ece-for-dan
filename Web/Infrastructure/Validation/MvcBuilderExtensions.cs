using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.Web.Infrastructure.Middleware.Exceptions;

namespace MoE.ECE.Web.Infrastructure.Validation
{
    public static class MvcBuilderExtensions
    {
        /// <summary>
        /// Adds fluent validation services to the mvc pipeline and enables validation
        /// interception to include error codes.
        /// </summary>
        /// <seealso cref="UseErrorCodeInterceptor"/>
        public static IMvcBuilder AddCodedFluentValidation(
            this IMvcBuilder builder,
            Action<FluentValidationMvcConfiguration> options)
        {
            // This will enable our error code interceptor to be included in the validation pipeline.
            builder.AddFluentValidation(options);

            // Parse any intercepted errors to include the error code in the outputted details.
            builder.Services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Any())
                        .Select(e => e.ToError());

                    return new BadRequestObjectResult(new ErrorResponse {Errors = errors});
                };
            });

            return builder;
        }

        private static Error ToError(this KeyValuePair<string, ModelStateEntry> error)
        {
            var messageParts = error.Value.Errors.First().ErrorMessage.Split('|');

            // Built-in data annotations do not get intercepted with an error code attached
            // to the error message; so we have to check.
            var hasErrorCode = messageParts.Count() == 2;

            return new Error
            {
                ErrorCode = hasErrorCode ? messageParts[0] : null,
                Property = error.Key,
                Message = hasErrorCode ? messageParts[1] : messageParts[0]
            };
        }
    }
}