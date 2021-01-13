using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace MoE.ECE.CLI.Commands
{
    public class PostgresCommands
    {
        private readonly IConfiguration _configuration;
        private const string MartenOptionsConnectionString = "MartenOptions:ConnectionString";

        public PostgresCommands(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public Command AddManagedIdentity
        {
            get
            {
                var command = new Command("add-app-login",
                    "Add local login to the database and access for the app.")
                {
                    new Option<string?>(new[] {"-c", "--connection-string"}, "database connection string"),
                    new Option<string>(new[] {"-p", "--postgresdb-connection-string"}, "postgres db connection string")
                    {
                        Argument = new Argument<string>
                        {
                            Arity = ArgumentArity.ExactlyOne
                        }
                    },
                    new Option<string>(new[] {"-r", "--rolename"}, "rolename to use against the database")
                    {
                        Argument = new Argument<string>
                        {
                            Arity = ArgumentArity.ExactlyOne
                        }
                    },
                    new Option<string>(new[] {"-w", "--password"}, "the password for the login")
                    {
                        Argument = new Argument<string>
                        {
                            Arity = ArgumentArity.ExactlyOne
                        }
                    },
                    new Option<string>(new[] {"-d", "--database"}, "the database to add the identity to")
                    {
                        Argument = new Argument<string>
                        {
                            Arity = ArgumentArity.ExactlyOne
                        }
                    },
                    new Option<string>(new[] {"-s", "--schema"},
                        "the initial schema to add access to. Additional schemas can be added using --add-managed-identity-to-schema")
                    {
                        Argument = new Argument<string>
                        {
                            Arity = ArgumentArity.ExactlyOne
                        }
                    }
                };
                command.Handler = CommandHandler.Create<string?, string, string, string, string, string>(AddApplicationAccessToDatabase);
                return command;
            }
        }
        public Command AddToSchema
        {
            get
            {
                var command = new Command("add-access-to-schema",
                    "Add access at schema level to the database.")
                {
                    new Option<string?>(new[] {"-c", "--connection-string"}, "database connection string"),
                    new Option<string>(new[] {"--name", "-n"}, "the name of the managedIdentity")
                    {
                        Argument = new Argument<string>
                        {
                            Arity = ArgumentArity.ExactlyOne
                        }
                    },
                    new Option<string>(new[] {"--schema", "-s"},
                        "the initial schema to add access to. Additional schemas can be added using --add-managed-identity-to-schema")
                    {
                        Argument = new Argument<string>
                        {
                            Arity = ArgumentArity.ExactlyOne
                        }
                    },
                    new Option<string>(new[] {"--grant-on-current", "-g"},
                "the initial schema to add access to. Additional schemas can be added using --add-managed-identity-to-schema")
                {
                    Argument = new Argument<string>
                    {
                        Arity = ArgumentArity.ExactlyOne
                    }
                },
                    new Option<string>(new[] {"--grant-on-future", "-f"},
                        "the initial schema to add access to. Additional schemas can be added using --add-managed-identity-to-schema")
                    {
                        Argument = new Argument<string>
                        {
                            Arity = ArgumentArity.ExactlyOne
                        }
                    }
                };
                command.Handler = CommandHandler.Create<string?, string, string, string, string>(AddAccessToSchema);
                return command;
            }
        }
        public Command AddUser
        {
            get
            {
                var command = new Command("add-user",
                    "Add an AD user or group to the database.")
                {
                    new Option<string?>(new[] {"-c", "--connection-string"}, "database connection string"),
                    new Option<string>(new[] {"-p", "--postgresdb-connection-string"}, "postgres db connection string")
                    {
                        Argument = new Argument<string>
                        {
                            Arity = ArgumentArity.ExactlyOne
                        }
                    },
                    new Option<string>(new[] {"-n", "--name"}, "the name of the managedIdentity")
                    {
                        Argument = new Argument<string>
                        {
                            Arity = ArgumentArity.ExactlyOne
                        }
                    },
                    new Option<string>(new[] {"-d", "--database"}, "the database to add the identity to")
                    {
                        Argument = new Argument<string>
                        {
                            Arity = ArgumentArity.ExactlyOne
                        }
                    },
                    new Option<string>(new[] {"-s", "--schema"},
                        "the initial schema to add access to. Additional schemas can be added using --add-managed-identity-to-schema")
                    {
                        Argument = new Argument<string>
                        {
                            Arity = ArgumentArity.ExactlyOne
                        }
                    }
                };

                command.Handler = CommandHandler.Create<string?, string, string, string, string>(AddUserToRole);

                return command;
            }
        }

        // Mainly meant for AD objects (users, groups, identities)
        private async Task<int> AddUserToRole(string? connectionString, string postgresdbConnectionString, string name, string database, string schema)
        {
            var result = await ConnectAndSendCommands(postgresdbConnectionString,
                new List<string>
                {
                    $@"SET aad_validate_oids_in_tenant = off;",
                    $@"DO $$
                       BEGIN
                         IF EXISTS (SELECT 1 FROM pg_roles  WHERE rolname  = '{name}') THEN
                            ALTER ROLE ""{name}"" WITH LOGIN;
                         ELSE
                            CREATE ROLE ""{name}"" WITH LOGIN IN ROLE azure_ad_user;
                         END IF;
                       END
                       $$;",
                    $@"GRANT CONNECT ON DATABASE {database} TO ""{name}"";"
                });
            if (result != 0)
            {
                return result;
            }
            return await AddAccessToSchema(connectionString, name, schema, "SELECT", "SELECT");
        }
        
        // Mainly meant for local postgresql accounts used by the application and own the database
        private async Task<int> AddApplicationAccessToDatabase(string? connectionString, string postgresdbConnectionString, string roleName, string password, string database, string schema)
        {
            var result = await ConnectAndSendCommands(postgresdbConnectionString,
                new List<string>
                {
                    $@"DO $$
                       BEGIN
                         IF EXISTS (SELECT 1 FROM pg_roles  WHERE rolname  = '{roleName}') THEN
                             ALTER ROLE ""{roleName}"" WITH LOGIN PASSWORD '{password}';
                         ELSE
                             CREATE ROLE ""{roleName}"" WITH LOGIN PASSWORD '{password}';
                         END IF;
                       END
                       $$;",
                    $@"GRANT CONNECT ON DATABASE {database} TO ""{roleName}"";",
                    $@"GRANT ALL PRIVILEGES ON DATABASE {database} TO ""{roleName}"";",
                    $@"GRANT ""{roleName}"" TO session_user;",
                    $@"ALTER DATABASE {database} OWNER to ""{roleName}"";",
                    $@"DO $$
                       BEGIN
                         IF EXISTS (SELECT 1 FROM information_schema.schemata WHERE schema_name = 'hangfire') THEN
                            ALTER SCHEMA hangfire OWNER TO ""{roleName}"";
                         END IF;
                       END
                       $$;",
                    $@"REVOKE ""{roleName}"" FROM session_User;"
                });
            if (result != 0)
            {
                return result;
            }
            return await AddAccessToSchema(connectionString, roleName, schema, "ALL", "ALL PRIVILEGES");
        }

        private async Task<int> AddAccessToSchema(string? connectionString, string name, string schema, string grantOnCurrent, string grantOnFuture)
        {
            return await ConnectAndSendCommands(connectionString,
                new List<string>
                {
                    $@"DO $$
                       BEGIN
                         IF EXISTS(SELECT 1 FROM information_schema.schemata WHERE schema_name = '{schema}') THEN
                            GRANT USAGE ON SCHEMA ""{schema}"" to ""{name}"";
                            GRANT {grantOnCurrent} ON ALL TABLES IN SCHEMA ""{schema}"" to ""{name}"";
                            GRANT {grantOnCurrent} ON ALL SEQUENCES IN SCHEMA ""{schema}"" to ""{name}"";
                            ALTER DEFAULT PRIVILEGES IN SCHEMA ""{schema}"" GRANT {grantOnFuture} ON TABLES TO ""{name}"";
                            ALTER DEFAULT PRIVILEGES IN SCHEMA ""{schema}"" GRANT {grantOnFuture} ON SEQUENCES TO ""{name}"";
                         END IF;
                       END
                    $$;"
                });
        }
        
        private async Task<int> ConnectAndSendCommands(string? connectionString, List<string> commands)
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                    connectionString = _configuration[MartenOptionsConnectionString];
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();
                //run on postgres
                await ExecuteCommands(connection, commands);
                return await Task.FromResult(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return await Task.FromResult(-2);
            }
        }

        private static Task ExecuteCommands(NpgsqlConnection connection, List<string> commands)
        {
            commands.ForEach(async command =>
            {
                try
                {
                    await using var cmd = new NpgsqlCommand(command, connection);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"command - {command} - failed : {e}");
                    throw;
                }
            });
            return Task.CompletedTask;
        }
    }
}