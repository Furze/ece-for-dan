using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.CLI.Commands;

namespace MoE.ECE.CLI
{
    internal class Program
    {
        private static ServiceProvider _serviceProvider = null!;
        private static IConfigurationRoot? _configuration;

        public static async Task<int> Main(string[] args)
        {
            _serviceProvider = ConfigureServices();

            // appsettings.json renamed because referencing the Web project causes a clash with its appsettings.json
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("cli-appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            var rootCommand = ConfigureRootCommand();
            try
            {
                // Parse the incoming args and invoke the handler
                return Environment.ExitCode = await rootCommand.InvokeAsync(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Environment.ExitCode = 1;
            }
        }

        private static ServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.Scan(scan => scan
                .FromAssembliesOf(
                    typeof(Domain.IAssemblyMarker))
                .AddClasses()
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services.BuildServiceProvider();
        }

        private static RootCommand ConfigureRootCommand()
        {
            if (_configuration == null) throw new InvalidOperationException("Cannot run with out configuration");

            var martenCommands = new MartenCommands(_configuration);
            var evolveCommands = new EvolveCommands(_configuration);
            var entityFrameworkCommands = new EntityFrameworkCommands(_serviceProvider, _configuration);
            var authorisationReportCommands = new AuthorisationReportCommands();

            // Create a root command with some options
            var rootCommand = new RootCommand
            {
                martenCommands.Apply,
                martenCommands.Assert,
                martenCommands.Patch,
                evolveCommands.Erase,
                evolveCommands.Info,
                evolveCommands.Migrate,
                evolveCommands.Repair,
                entityFrameworkCommands.MigrateReferenceData,
                entityFrameworkCommands.CreateSeedCommand(),
                authorisationReportCommands.AuthorisationReportCommand,
            };

            rootCommand.Description = "ECE API Migrations";

            return rootCommand;
        }
    }
}