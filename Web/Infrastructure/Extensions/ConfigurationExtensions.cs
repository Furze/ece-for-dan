using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MoE.ECE.Web.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Configures the given type as being available for dependency injection as an IOption instance.
        /// </summary>
        public static IServiceCollection ConfigureFor<T>(
            this IServiceCollection services,
            IConfiguration configuration)
            where T : class
        {
            var section = configuration.GetSection(typeof(T).Name);

            services.AddOptions<T>()
                .Bind(section)
                .ValidateDataAnnotations();
            
            //services.Configure<T>(section);
            
            return services;
        }

        /// <summary>
        /// Gets the config section for the given generic type.
        /// </summary>
        public static T BindFor<T>(this IConfiguration configuration)
            where T : new()
        {
            var settings = new T();
            configuration
                .GetSection(typeof(T).Name)
                .Bind(settings);

            return settings;
        }
    }
}