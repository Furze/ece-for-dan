using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace MoE.ECE.Web.Bootstrap
{
    /// <summary>
    /// Responsible for configuring where application secrets are sourced from.
    /// </summary>
    public interface ISecretStoreSetup
    {
        /// <summary>
        /// Allow secrets retrieval to be configured against the given configuration builder.
        /// </summary>
        void Configure(IConfigurationRoot configuration, IConfigurationBuilder builder, WebHostBuilderContext context);
    }
}