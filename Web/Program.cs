using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MoE.ECE.Web.Bootstrap;
using MoE.ECE.Web.Infrastructure.Extensions;
using Serilog.Debugging;

namespace MoE.ECE.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SelfLog.Enable(Console.Error);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureLogging()
                        .UseConfiguration(new AzureSecretStoreSetup())
                        .UseStartup<Startup>();
                });
    }
}