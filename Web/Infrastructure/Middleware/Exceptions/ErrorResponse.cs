using System.Collections.Generic;

namespace MoE.ECE.Web.Infrastructure.Middleware.Exceptions
{
    /// <summary>
    /// The standardised response object returned from an invalid web operation.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets the collection of errors returned from an api request.
        /// </summary>
        public IEnumerable<Error>? Errors { get; set; }
    }
}