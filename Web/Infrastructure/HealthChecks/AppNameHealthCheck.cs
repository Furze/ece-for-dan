using System.Threading;
using System.Threading.Tasks;
using App.Metrics.Health;

namespace MoE.ECE.Web.Infrastructure.HealthChecks
{
    public class AppNameHealthCheck : HealthCheck
    {
        private const string HealthCheckName = "EntryAssemblyName";
        
        public AppNameHealthCheck()
            : base(HealthCheckName)
        {
        }

        protected override ValueTask<HealthCheckResult> CheckAsync(CancellationToken cancellationToken = default)
        {
            return new ValueTask<HealthCheckResult>(HealthCheckResult.Healthy("ECE API"));
        }
    }
}