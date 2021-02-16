using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using Marten;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Infrastructure;
using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Web.Bootstrap;

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
        
        public Command AddTestData()
        {
            var testData = new Command("test-data", "Add the test data to the database.")
            {
                new Option<string?>(new[] {"-cs", "--connection-string"},
                    "The connection string to use (optional)")
            };
            testData.Handler = CommandHandler.Create<string?>(TestData);
            return testData;
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
            
            var documentStore = BuildDocumentStore(connectionString);

            using var session = documentStore.LightweightSession();
            
            var referenceData = new ReferenceData(referenceDataContext, session);
            
            referenceData.SeedData();

            referenceDataContext.SaveChanges();
        }

        private void TestData(string? connectionString)
        {
            var referenceDataContext = CreateReferenceDataContext(connectionString);
            
            var documentStore = BuildDocumentStore(connectionString);

            using var session = documentStore.LightweightSession();
            
            var referenceData = new ReferenceData(referenceDataContext, session);
            
            referenceData.TestData();
        }

        private IDocumentStore BuildDocumentStore(string? connectionString = null)
        {
            var martenSettings = _configuration.BindFor<MartenSettings>();

            if (string.IsNullOrEmpty(connectionString))
                connectionString = martenSettings.ConnectionString;

            martenSettings.ConnectionString = connectionString;

            Migrations.EnsureDatabaseExists(connectionString);

            return DocumentStore.For(
                options => EceMartenRegistry.ApplyDefaultConfiguration(options, martenSettings));
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
                _serviceProvider.GetService<IEnumerable<IEntityConfiguration>>()!);
            return referenceDataContext;
        }
    }
}
