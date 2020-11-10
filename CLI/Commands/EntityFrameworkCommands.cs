using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Infrastructure;
using MoE.ECE.Domain.Infrastructure.EntityFramework;

namespace MoE.ECE.CLI.Commands
{
    public class EntityFrameworkCommands
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceProvider _serviceProvider;

        public EntityFrameworkCommands(ServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public Command MigrateReferenceData
        {
            get
            {
                var command = new Command("migrate-reference-data",
                    "Apply the latest reference data migrations to the database")
                {
                    new Option<string?>(new[] {"-cs", "--connection-string"},
                        "The connection string to use (optional)")
                };

                command.Handler = CommandHandler.Create<string?>(ApplyReferenceDataMigrations);
                return command;
            }
        }

        public Command CreateSeedCommand()
        {
            var seed = new Command("seed", "Add the seed data to the database.")
            {
                new Option<string?>(new[] {"-cs", "--connection-string"},
                    "The connection string to use (optional)")
            };
            seed.Handler = CommandHandler.Create<string?>(SeedData);
            return seed;
        }

        private void ApplyReferenceDataMigrations(string? connectionString)
        {
            var referenceDataContext = CreateReferenceDataContext(connectionString);

            var pendingMigrations = referenceDataContext.Database.GetPendingMigrations().ToList();
            Console.WriteLine($"Found {pendingMigrations.Count} pending migrations");

            if (!pendingMigrations.Any()) return;

            Console.WriteLine("Applying migrations...");

            referenceDataContext.Database.Migrate();
        }

        private void SeedData(string? connectionString)
        {
            var referenceDataContext = CreateReferenceDataContext(connectionString);

            var referenceData = new ReferenceData(referenceDataContext);
            
            referenceData.SeedData();

            referenceDataContext.SaveChanges();
        }

        private ReferenceDataContext CreateReferenceDataContext(string? connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                var martenSettings = _configuration.BindFor<MartenSettings>();
                connectionString = martenSettings.ConnectionString;

                Console.WriteLine($"Upgrading the database using the default connection string: {connectionString}");
            }

            var optionsBuilder = new DbContextOptionsBuilder<ReferenceDataContext>();
            optionsBuilder
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();

            var referenceDataContext = new ReferenceDataContext(optionsBuilder.Options,
                _serviceProvider.GetService<IEnumerable<IEntityConfiguration>>());
            return referenceDataContext;
        }
    }
}
