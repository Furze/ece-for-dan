using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MoE.ECE.Domain.Infrastructure;
using Npgsql;

namespace MoE.ECE.CLI.Commands
{
    public class EvolveCommands
    {
        private readonly IConfiguration _configuration;

        public EvolveCommands(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Command Migrate
        {
            get
            {
                var command = new Command("migrate", "Use Evolve to apply the SQL migration file(s)")
                {
                    new Option<string?>(new[] {"-cs", "--connection-string"},
                        "The connection string to use (optional)"),
                    new Option<string?>(new[] {"-md", "--migrations-directory"},
                        "The directory to read the migration files from (mandatory)")
                };

                command.Handler = CommandHandler.Create<string?, string?>(EvolveMigrate);
                return command;
            }
        }

        public Command Erase
        {
            get
            {
                var command = new Command("erase",
                    "Use Evolve to erase the database schema(s) if Evolve has created it or has found it empty.")
                {
                    new Option<string?>(new[] {"-cs", "--connection-string"},
                        "The connection string to use (optional)"),
                    new Option<string?>(new[] {"-md", "--migrations-directory"},
                        "The directory to read the migration files from (mandatory)")
                };

                command.Handler = CommandHandler.Create<string?, string?>(EvolveErase);
                return command;
            }
        }

        public Command Repair
        {
            get
            {
                var command = new Command("repair",
                    "Use Evolve to Correct checksums of already applied migrations, with the ones from actual migration scripts.")
                {
                    new Option<string?>(new[] {"-cs", "--connection-string"},
                        "The connection string to use (optional)"),
                    new Option<string?>(new[] {"-md", "--migrations-directory"},
                        "The directory to read the migration files from (mandatory)")
                };

                command.Handler = CommandHandler.Create<string?, string?>(EvolveRepair);
                return command;
            }
        }

        public Command Info
        {
            get
            {
                var command = new Command("info",
                    "Use Evolve to display the details and status information about all the migrations")
                {
                    new Option<string?>("-cs", "--connection-string"),
                    new Option<string?>(new[] {"-md", "--migrations-directory"},
                        "The directory to read the migration files from")
                };

                command.Handler = CommandHandler.Create<string?, string?>(EvolveInfo);
                return command;
            }
        }

        private async Task<int> EvolveMigrate(string? connectionString, string? migrationsDirectory)
        {
            if (NoMigrationDirectoryPassed(migrationsDirectory))
                return await Task.FromResult(-1);

            Console.WriteLine($"Migrating DB from {migrationsDirectory}");

            CreateEvolveMigrator(connectionString, migrationsDirectory!).Migrate();
            Console.WriteLine("Migration completed OK");
            return await Task.FromResult(0);
        }

        private async Task<int> EvolveErase(string? connectionString, string? migrationsDirectory)
        {
            if (NoMigrationDirectoryPassed(migrationsDirectory))
                return await Task.FromResult(-1);

            Console.WriteLine("Erasing DB...");
            CreateEvolveMigrator(connectionString, migrationsDirectory!).Erase();
            Console.WriteLine("Erase completed OK");
            return await Task.FromResult(0);
        }

        private async Task<int> EvolveRepair(string? connectionString, string? migrationsDirectory)
        {
            if (NoMigrationDirectoryPassed(migrationsDirectory))
                return await Task.FromResult(-1);

            Console.WriteLine($"Attempting repair. Migrations directory: {migrationsDirectory}");

            CreateEvolveMigrator(connectionString, migrationsDirectory!).Repair();
            Console.WriteLine("Repair completed OK");
            return await Task.FromResult(0);
        }

        private async Task<int> EvolveInfo(string? connectionString, string? migrationsDirectory)
        {
            if (NoMigrationDirectoryPassed(migrationsDirectory))
                return await Task.FromResult(-1);

            Console.WriteLine($"Obtaining info. Migrations directory: {migrationsDirectory}");
            CreateEvolveMigrator(connectionString, migrationsDirectory!).Info();
            Console.WriteLine("Info completed OK");
            return await Task.FromResult(0);
        }

        private Evolve.Evolve CreateEvolveMigrator(string? connectionString, string migrationsDirectory)
        {
            var martenSettings = _configuration.BindFor<MartenSettings>();

            if (string.IsNullOrEmpty(connectionString))
                connectionString = martenSettings.ConnectionString;

            Console.WriteLine($"using connection string: {connectionString}");
            Migrations.EnsureDatabaseExists(connectionString);

            var evolve = new Evolve.Evolve(new NpgsqlConnection(connectionString));
            if (!string.IsNullOrEmpty(migrationsDirectory))
                evolve.Locations = new[] {migrationsDirectory};

            Migrations.EchoMigrationsDirectory(migrationsDirectory);

            return evolve;
        }

        private bool NoMigrationDirectoryPassed(string? migrationsDirectory)
        {
            if (migrationsDirectory != null)
                return false;

            Console.WriteLine("You must provide the directory name to read the patch from -md");
            return true;
        }
    }
}