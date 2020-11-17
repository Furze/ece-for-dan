using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Logging;
using MoE.ECE.Domain.Infrastructure.Abstractions;
using MoE.ECE.Domain.Integration;
using Moe.ECE.Events.Integration;
using MoE.ECE.Web.Infrastructure;
using Moe.Library.Cqrs;
using Serilog;
using Serilog.Events;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RunOnceBeforeAllTests : IDisposable
    {
        private static string _connectionString = "";

        public RunOnceBeforeAllTests()
        {
            _connectionString = DatabaseManager.Start();

            Host = CreateHost();

            Services = Host.Services;
            HttpClient = Host.GetTestClient();
        }

        public IHost Host { get; }

        public IServiceProvider Services { get; }

        public HttpClient HttpClient { get; }

        private static Dictionary<string, string> InMemoryConfiguration => new Dictionary<string, string>
        {
            {"MartenSettings:ConnectionString", _connectionString}
        };

        public void Dispose()
        {
            Log.CloseAndFlush();

            Host.Dispose();

            HttpClient.Dispose();
        }

        private static IHost CreateHost()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(LogEventLevel.Verbose)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Hangfire", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Environment", "ECE API Integration Test")
                .WriteTo.Seq("http://localhost:5341")
                .WriteTo.Console()
                .CreateLogger();

            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(builder =>
                    builder
                        .UseSerilog()
                        .UseStartup<TestStartup>()
                        .UseTestServer()
                        .UseEnvironment("development")
                        .ConfigureAppConfiguration((context, configBuilder) =>
                        {
                            configBuilder.AddJsonFile("appsettings.json", true, true);
                            configBuilder.AddJsonFile("appsettings.integration-test.json", false, true);
                            configBuilder.AddInMemoryCollection(InMemoryConfiguration);
                            configBuilder.AddEnvironmentVariables();
                            configBuilder.AddUserSecrets<TestStartup>();
                        })
                        .ConfigureTestServices(ServicesConfiguration));

            Log.Information("******* TEST API STARTING *******");
            var host = hostBuilder.Start();
            return host;
        }

        private static void ServicesConfiguration(IServiceCollection services)
        {
            // Replace CQRS with tracking/decorated version
            RemoveDependency<ICqrs>(services);
            services.AddScoped<MediatorCqrs>();
            services.AddScoped<ICqrs, TrackingMediatorCqrs>();

            // Replace IServiceBus with tracking/decorated version
            RemoveDependency<IServiceBus>(services);
            services.AddScoped<InMemoryServiceBus>();
            services.AddScoped<IServiceBus, TrackingInMemoryServiceBus>();

            services.AddScoped<IEventTracker<IDomainEvent>, EventTracker<IDomainEvent>>();
            services.AddScoped<IEventTracker<IIntegrationEvent>, EventTracker<IIntegrationEvent>>();
            services.AddMemoryCache();

            RemoveDependency<ILoggedOnUser>(services);
            services.AddScoped<ILoggedOnUser, FakeLoggedOnUser>();
            RemoveDependency<ISystemClock>(services);
            services.AddScoped<ISystemClock, FakeSystemClock>();

            services.AddLogging(builder => builder.AddSeq());
        }

        private static void RemoveDependency<T>(IServiceCollection services) =>
            GetDependencyDescriptor<T>(services)
                .ToList()
                .ForEach(dependency => services.Remove(dependency));

        /// <summary>
        ///     Gets the service description details for the given type.
        /// </summary>
        private static IEnumerable<ServiceDescriptor>
            GetDependencyDescriptor<TDependency>(IServiceCollection services) =>
            services.Where(t => t.ServiceType == typeof(TDependency));
    }
}