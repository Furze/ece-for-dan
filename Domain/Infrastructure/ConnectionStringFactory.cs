using System;
using Microsoft.Extensions.Configuration;

namespace MoE.ECE.Domain.Infrastructure
{
    public class ConnectionStringFactory : IConnectionStringFactory
    {
        private const string PasswordToken = "{{PASSWORD_FROM_KEYVAULT}}";
        private const string ECESqlPassword = "ece-sqlpassword";
        private readonly IConfiguration _configuration;
        
        public ConnectionStringFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            var connectionString = _configuration.GetSection("MartenSettings")["ConnectionString"];

            if (connectionString.Contains(PasswordToken))
            {
                var password = _configuration.GetValue<string>(ECESqlPassword);
                
                if (password == null)
                    throw new Exception($"Key {ECESqlPassword} has not been set in key vault.");

                connectionString = connectionString.Replace(PasswordToken, password);
            }

            return connectionString;
        }
    }
}