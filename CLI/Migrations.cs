using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Npgsql;

namespace MoE.ECE.CLI
{
    public static class Migrations
    {
        public const string MigrationFileRootDirectoryName = "migrations";
        public const string MigrationsDropFileSubDirectory = "drop";
        public const string VersionedMigrationPrefix = "V";
        public const string SqlFileExtension = ".sql";

        public static IEnumerable<string> GetMigrationFiles(string migrationDirectory) =>
            Directory.GetFiles(migrationDirectory)
                // ReSharper disable once ConstantNullCoalescingCondition
                .Select(f => Path.GetFileName(f) ?? "")
                .Where(f => f.StartsWith(VersionedMigrationPrefix) && f.EndsWith(SqlFileExtension));

        public static void EchoMigrationsDirectory(string migrationsDirectory)
        {
            Console.WriteLine("Migrations files found:");
            Console.WriteLine(
                string.Join($"{Environment.NewLine} - ", GetMigrationFiles(migrationsDirectory)));
        }

        public static void EnsureDatabaseExists(string connectionString)
        {
            NpgsqlConnectionStringBuilder? connectionStringBuilder =
                new(connectionString);
            string? database = connectionStringBuilder.Database;
            if (string.IsNullOrEmpty(database) || database.Trim() == string.Empty)
            {
                throw new InvalidOperationException("The connection string does not specify a database name.");
            }

            using NpgsqlConnection? connection = new(connectionStringBuilder.ConnectionString);
            connection.Open();

            using NpgsqlCommand? existsCommand = new(
                $"SELECT case WHEN oid IS NOT NULL THEN 1 ELSE 0 end FROM pg_database WHERE datname = '{database}' limit 1;",
                connection) {CommandType = CommandType.Text};

            int result = (int)existsCommand.ExecuteScalar();
            if (result == 1)
            {
                return;
            }

            using NpgsqlCommand? createDatabaseCommand =
                new($"create database \"{database}\";", connection) {CommandType = CommandType.Text};
            createDatabaseCommand.ExecuteNonQuery();

            Console.WriteLine($"Created database {database}");
        }
    }
}