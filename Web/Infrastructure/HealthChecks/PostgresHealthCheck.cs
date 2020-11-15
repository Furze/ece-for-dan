﻿using System;
using System.Threading;
using System.Threading.Tasks;
using App.Metrics.Health;
using MoE.ECE.Domain.Infrastructure;
using Npgsql;

namespace MoE.ECE.Web.Infrastructure.HealthChecks
{
    public class PostgresHealthCheck : HealthCheck
    {
        private const string HealthCheckName = "Postgres Health Check";
        private readonly IConnectionStringFactory _connectionStringFactory;

        public PostgresHealthCheck(IConnectionStringFactory connectionStringFactory) : base(HealthCheckName) =>
            _connectionStringFactory = connectionStringFactory;

        protected override async ValueTask<HealthCheckResult> CheckAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                string? connectionString = _connectionStringFactory.GetConnectionString();

                await using (NpgsqlConnection? connection = new(connectionString))
                {
                    await connection.OpenAsync(cancellationToken);
                }

                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(ex);
            }
        }
    }
}