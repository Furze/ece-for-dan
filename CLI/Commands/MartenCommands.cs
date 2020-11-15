using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Marten;
using Marten.Schema;
using Microsoft.Extensions.Configuration;
using MoE.ECE.Domain.Infrastructure;
using MoE.ECE.Web.Bootstrap;

namespace MoE.ECE.CLI.Commands
{
    public class MartenCommands
    {
        private readonly IConfiguration _configuration;

        public MartenCommands(IConfiguration configuration) => _configuration = configuration;

        public Command Apply
        {
            get
            {
                Command? command = new("apply",
                    "Use Marten to apply all outstanding changes to the database based on the current Marten configuration")
                {
                    new Option<string?>("-cs", "--connection-string")
                };

                command.Handler = CommandHandler.Create<string?>(MartenApply);

                return command;
            }
        }

        public Command Assert
        {
            get
            {
                Command? command = new("assert",
                    "Use Marten to assert that the existing database matches the current Marten configuration")
                {
                    new Option<string?>("-cs", "--connection-string")
                };

                command.Handler = CommandHandler.Create<string?>(MartenAssert);

                return command;
            }
        }

        public Command Patch
        {
            get
            {
                Command? command = new("patch",
                    "Use Marten to evaluate the current configuration against the database and write a patch and drop file if there are any differences")
                {
                    new Option<string?>(new[] {"-cs", "--connection-string"}, "The PG connection string"),
                    new Option<string?>(new[] {"-pn", "--patch-name"}, "The name for the patch"),
                    new Option<string?>(new[] {"-so", "--schema-objects"},
                        "Opt into also writing out any missing schema creation scripts"),
                    new Option<string?>(new[] {"-md", "--migrations-directory"},
                        "The directory to write migration files to"),
                    new Option<string?>(new[] {"-t", "--transactional"},
                        "Option to create scripts as transactional script")
                };

                command.Handler = CommandHandler.Create<string?, string?, string?, string?, string?>(MartenPatch);

                return command;
            }
        }

        private async Task<int> MartenApply(string? connectionString)
        {
            IDocumentStore? store = BuildDocumentStore(connectionString);
            store.Schema.ApplyAllConfiguredChangesToDatabase();
            Console.WriteLine("Successfully applied outstanding database changes");
            return await Task.FromResult(0);
        }

        private async Task<int> MartenAssert(string? connectionString)
        {
            IDocumentStore? store = BuildDocumentStore(connectionString);
            try
            {
                store.Schema.AssertDatabaseMatchesConfiguration();
                Console.WriteLine("No database differences detected.");
                return await Task.FromResult(0);
            }
            catch (SchemaValidationException e)
            {
                Console.WriteLine("The database does not match the configuration!");
                Console.WriteLine(e.ToString());
                Console.WriteLine(
                    "The changes are the patch describing the difference between the database and the current configuration");
                return await Task.FromResult(-1);
            }
            catch (Exception e)
            {
                Console.WriteLine("The database does not match the configuration!");
                Console.WriteLine(e.ToString());
                return await Task.FromResult(-2);
            }
        }

        private async Task<int> MartenPatch(
            string? patchName,
            string? migrationsDirectory,
            string? schemaObjects,
            string? transactional,
            string? connectionString = null)
        {
            if (patchName == null)
            {
                Console.WriteLine("You must provide the name of your patch -pn");
                return await Task.FromResult(-1);
            }

            if (migrationsDirectory == null)
            {
                Console.WriteLine("You must provide the directory name to write the patch to -md");
                return await Task.FromResult(-1);
            }

            IDocumentStore? store = BuildDocumentStore(connectionString);
            try
            {
                store.Schema.AssertDatabaseMatchesConfiguration();
                Console.WriteLine("No differences were detected between the Marten configuration and the database");
                Migrations.EchoMigrationsDirectory(migrationsDirectory);
                return await Task.FromResult(0);
            }
            catch (SchemaValidationException)
            {
                return await BuildPatchFiles(patchName, migrationsDirectory, schemaObjects ?? "false",
                    transactional ?? "false", store);
            }
        }

        private async Task<int> BuildPatchFiles(string patchName, string migrationsDirectory, string schemaObjects,
            string transactional, IDocumentStore store)
        {
            bool scriptSchemaCreationFlag = bool.Parse(schemaObjects);
            bool transactionalScriptFlag = bool.Parse(transactional);

            SchemaPatch? patch = store.Schema.ToPatch(scriptSchemaCreationFlag, true);

            string? baseMigrationsDirectory =
                Path.Combine(migrationsDirectory, Migrations.MigrationFileRootDirectoryName);
            Directory.CreateDirectory(baseMigrationsDirectory);

            string? basePatchFile = GetPatchFileName(baseMigrationsDirectory, patchName) + Migrations.SqlFileExtension;
            string? upFileWithPath = Path.Combine(baseMigrationsDirectory, basePatchFile);

            if (IfPatchFileAlreadyExists(patchName, baseMigrationsDirectory))
            {
                Console.WriteLine("Patch already exists. Use a unique name for the -pn argument");
                return await Task.FromResult(-1);
            }

            string? dropDirectory = Path.Combine(baseMigrationsDirectory, Migrations.MigrationsDropFileSubDirectory);
            Directory.CreateDirectory(dropDirectory);
            string? downFileWithPath =
                Path.Combine(dropDirectory, $"{Migrations.MigrationsDropFileSubDirectory}_{basePatchFile}");

            Console.WriteLine($"Wrote a patch file to {upFileWithPath}");
            patch.WriteUpdateFile(upFileWithPath, transactionalScriptFlag);

            Console.WriteLine("Wrote the drop file to " + downFileWithPath);
            patch.WriteRollbackFile(downFileWithPath, transactionalScriptFlag);

            return await Task.FromResult(0);
        }

        private static bool IfPatchFileAlreadyExists(string patchName, string baseMigrationsDirectory) => Directory
            .GetFiles(baseMigrationsDirectory, $"*{patchName}{Migrations.SqlFileExtension}").Length > 1;

        private string GetPatchFileName(string migrationsDirectory, string patchName)
        {
            int maxMigration = Migrations.GetMigrationFiles(migrationsDirectory)
                .Select(GetVersionPrefixOfFileName)
                .Select(int.Parse)
                .OrderBy(f => f)
                .LastOrDefault();

            int migrationNumber = maxMigration + 1;
            string? datePart = DateTimeOffset.Now.ToString("yyyyMMdd");
            return $"{Migrations.VersionedMigrationPrefix}{migrationNumber}__{datePart}_{patchName}";
        }

        private static string GetVersionPrefixOfFileName(string fileName) =>
            Regex.Match(fileName, $"^{Migrations.VersionedMigrationPrefix}" + @"\d*__")
                .Captures.FirstOrDefault()?.Value
                .Replace(Migrations.VersionedMigrationPrefix, "")
                .Replace("_", "")
            ?? "";

        private IDocumentStore BuildDocumentStore(string? connectionString = null)
        {
            MartenSettings? martenSettings = _configuration.BindFor<MartenSettings>();

            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = martenSettings.ConnectionString;
            }

            martenSettings.ConnectionString = connectionString;

            Migrations.EnsureDatabaseExists(connectionString);

            return DocumentStore.For(
                options => EceMartenRegistry.ApplyDefaultConfiguration(options, martenSettings));
        }
    }
}