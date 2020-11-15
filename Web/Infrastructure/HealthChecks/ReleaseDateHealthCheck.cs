using System.Threading;
using System.Threading.Tasks;
using App.Metrics.Health;
using Microsoft.Extensions.Configuration;

namespace MoE.ECE.Web.Infrastructure.HealthChecks
{
    public class ReleaseDateHealthCheck : HealthCheck
    {
        private const string HealthCheckName = "ReleaseDate";
        private readonly IConfiguration _config;
        
        public ReleaseDateHealthCheck(IConfiguration config)
            : base(HealthCheckName)
        {
            _config = config;
        }
        
        protected override ValueTask<HealthCheckResult> CheckAsync(CancellationToken cancellationToken = default)
        {
            var releaseDateValue = _config.GetValue<string>(HealthCheckName);
            
            return string.IsNullOrWhiteSpace(releaseDateValue) 
                ? new ValueTask<HealthCheckResult>(HealthCheckResult.Ignore($"{HealthCheckName} is unavailable via configuration, this should be set in the release pipeline.")) 
                : new ValueTask<HealthCheckResult>(HealthCheckResult.Healthy($"{releaseDateValue}"));
        }
    }
}