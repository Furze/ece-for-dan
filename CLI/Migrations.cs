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

        public static IEnumerable<string> GetMigrationFiles(string migrationDirectory)
        {
            if(Directory.Exists(migrationDirectory) == false)
                Console.WriteLine($"Directory does not exist: '{migrationDirectory}'");
            
            return Directory.GetFiles(migrationDirectory)
                // ReSharper disable once ConstantNullCoalescingCondition
                .Select(f => Path.GetFileName(f) ?? "")
                .Where(f => f.StartsWith(VersionedMigrationPrefix) && f.EndsWith(SqlFileExtension));
        }

        public static void EchoMigrationsDirectory(string migrationsDirectory)
        {
            Console.WriteLine("Migrations files found:");
            Console.WriteLine(
                string.Join($"{Environment.NewLine} - ", GetMigrationFiles(migrationsDirectory)));
        }

        public static void EnsureDatabaseExists(string connectionString)
        {
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);
            var database = connectionStringBuilder.Database;
            if (string.IsNullOrEmpty(database) || database.Trim() == string.Empty)
                throw new InvalidOperationException("The connection string does not specify a database name.");

            using var connection = new NpgsqlConnection(connectionStringBuilder.ConnectionString);
            connection.Open();

            using var existsCommand = new NpgsqlCommand(
                $"SELECT case WHEN oid IS NOT NULL THEN 1 ELSE 0 end FROM pg_database WHERE datname = '{database}' limit 1;",
                connection)
            {
                CommandType = CommandType.Text
            };

            var result = (int)existsCommand.ExecuteScalar();
            if (result == 1)
                return;

            using var createDatabaseCommand = new NpgsqlCommand($"create database \"{database}\";", connection)
            {
                CommandType = CommandType.Text
            };
            createDatabaseCommand.ExecuteNonQuery();

            Console.WriteLine($"Created database {database}");
        }
    }
}