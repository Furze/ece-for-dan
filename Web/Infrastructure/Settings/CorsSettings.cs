namespace MoE.ECE.Web.Infrastructure.Settings
{
    public class CorsSettings
    {
        public string[]? AllowedHeaders { get; set; }

        public string[]? AllowedMethods { get; set; }

        public string[]? AllowedOrigins { get; set; }

        public string[]? ExposedHeaders { get; set; }

        public bool AllowCredentials { get; set; }
    }
}