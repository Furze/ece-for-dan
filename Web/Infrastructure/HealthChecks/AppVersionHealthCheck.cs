using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using App.Metrics.Health;

namespace MoE.ECE.Web.Infrastructure.HealthChecks
{
    public class AppVersionHealthCheck : HealthCheck
    {
        private const string HealthCheckName = "EntryAssemblyVersion";
        
        public AppVersionHealthCheck()
            : base(HealthCheckName)
        {
        }

        protected override ValueTask<HealthCheckResult> CheckAsync(CancellationToken cancellationToken = default)
        {
            var versionNumber = Assembly.GetEntryAssembly()?.GetName().Version?.ToString();
            return string.IsNullOrWhiteSpace(versionNumber)
                ? new ValueTask<HealthCheckResult>(HealthCheckResult.Ignore($"{HealthCheckName} is unavailable."))
                : new ValueTask<HealthCheckResult>(HealthCheckResult.Healthy($"{versionNumber}"));
        }
    }
}