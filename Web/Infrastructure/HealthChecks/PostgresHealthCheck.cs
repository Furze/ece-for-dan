using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MoE.ECE.Domain.Infrastructure;
using Npgsql;

namespace MoE.ECE.Web.Infrastructure.HealthChecks
{
    public class PostgresHealthCheck : IHealthCheck
    {
        public const string HealthCheckName = "Postgres Health Check";
        private readonly IConnectionStringFactory _connectionStringFactory;

        public PostgresHealthCheck(IConnectionStringFactory connectionStringFactory)
        {
            _connectionStringFactory = connectionStringFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var connectionString = _connectionStringFactory.GetConnectionString();

                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync(cancellationToken);
                }

                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(exception: ex);
            }
        }
    }
}