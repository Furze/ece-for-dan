namespace MoE.ECE.Web.Infrastructure.Middleware.Exceptions
{
    /// <summary>
    /// The details for a single api request error.
    /// </summary>
    public class Error
    {
        public string? ErrorCode { get; set; }

        public string? Property { get; set; }

        public string? Message { get; set; }
    }
}