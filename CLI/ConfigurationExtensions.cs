using Microsoft.Extensions.Configuration;

namespace MoE.ECE.CLI
{
    public static class ConfigurationExtensions
    {
        public static T BindFor<T>(this IConfiguration configuration)
            where T : new()
        {
            T settings = new();
            configuration
                .GetSection(typeof(T).Name)
                .Bind(settings);

            return settings;
        }
    }
}