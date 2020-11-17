using System;
using Bard.Db;
using Marten;
using MoE.ECE.CLI;
using MoE.ECE.Domain.Infrastructure;
using Npgsql;
using Respawn;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    internal class DatabaseManager
    {
        /// <summary>
        ///     /// This password is only used to connect to a docker integration test db...So don't worry.
        /// </summary>
        private const string Password = "Password1";

        private const string DockerDatabaseContainerName = "ece_local_db";
        private const string UserName = "ece_api_docker_user";

        private static readonly Checkpoint _checkpoint = new Checkpoint
        {
            SchemasToInclude = new[] {"referencedata"},
            TablesToIgnore = new[]
            {
                "__EFMigrationsHistory",
                // "ece_service", "ece_licencing_detail_date_ranged_parameter",
                // "ece_operating_session", "ece_operating_session_date_ranged_parameter",
                // "ece_service_date_ranged_parameter", "lookup", "lookup_type"
            },
            DbAdapter = DbAdapter.Postgres
        };

        private static string? _hostIp;

        private readonly IDocumentStore _documentStore;

        public DatabaseManager(IDocumentStore documentStore) => _documentStore = documentStore;

        private static bool IsRunningOnBuildServer =>
            Environment.GetEnvironmentVariable("BUILD_DEFINITIONNAME") != null;

        private static string ConnectionString =>
            $"host={HostIp};port={PortNumber};database={DockerDatabaseContainerName};password={Password};username={UserName};Pooling=true;Include Error Detail=true;";

        private static string HostIp => IsRunningOnBuildServer == false || _hostIp == null ? "localhost" : _hostIp;

        private static string PortNumber => IsRunningOnBuildServer
            ? "5432"
            : "8432"; // Change to 8342 on dev machine to stop collisions if Postgres server running locally.

        public static string Start()
        {
            var postgresDb = new PostgresDatabase(DockerDatabaseContainerName, UserName, Password, PortNumber);

            _hostIp = postgresDb.StartDatabase();

            MigrateDatabases(ConnectionString);
            return ConnectionString;
        }

        private static void MigrateDatabases(string databaseConnectionString)
        {
            Console.WriteLine(Environment.CurrentDirectory);
            AsyncHelper.RunSync(() => Program.Main(new[]
            {
                "migrate", "-cs", databaseConnectionString, "-md",
                $"{Environment.CurrentDirectory}/../../../../../CLI/migrations"
            }));

            AsyncHelper.RunSync(() => Program.Main(new[] {"migrate-reference-data", "-cs", databaseConnectionString}));
        }

        public void ResetDatabase()
        {
            AsyncHelper.RunSync(async () =>
            {
                await using var conn = new NpgsqlConnection(ConnectionString);
                await conn.OpenAsync();
                await _checkpoint.Reset(conn);
            });

            _documentStore.Advanced.Clean.DeleteAllDocuments();
            _documentStore.Advanced.Clean.DeleteAllEventData();
            
            AsyncHelper.RunSync(() => Program.Main(new[] {"seed", "-cs", ConnectionString}));
        }
    }
}