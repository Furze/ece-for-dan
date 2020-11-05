using System;
using Marten;
using Marten.Services.Events;
using Marten.Storage;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.Domain.Infrastructure;

namespace MoE.ECE.Web.Bootstrap
{
    public class ECEMartenRegistry : MartenRegistry
    {
        public static void ApplyDefaultConfiguration(StoreOptions storeOptions, MartenSettings settings,
            IServiceProvider? serviceProvider = null)
        {
            if (string.IsNullOrEmpty(settings.SchemaName) == false)
                storeOptions.DatabaseSchemaName = settings.SchemaName;

            if (string.IsNullOrEmpty(settings.ConnectionString))
                throw new Exception("Configuration Error: MartenSettings:ConnectionString not set.");

            storeOptions.Connection(settings.ConnectionString);
            storeOptions.UseDefaultSerialization(nonPublicMembersStorage: NonPublicMembersStorage.NonPublicSetters);

            storeOptions.AutoCreateSchemaObjects = settings.AutoCreate;
            storeOptions.PLV8Enabled = false;
            storeOptions.Policies.ForAllDocuments(_ => _.TenancyStyle = TenancyStyle.Single);

            storeOptions.UseDefaultSerialization(EnumStorage.AsString);

            if (serviceProvider != null)
            {
                var telemetryClient = serviceProvider.GetService<TelemetryClient>();
                storeOptions.Logger(new AppInsightsMartenLogger(telemetryClient));
            }

            storeOptions.Schema.Include<ECEMartenRegistry>();

            EventConfiguration(storeOptions);
        }

        private static void EventConfiguration(StoreOptions storeOptions)
        {
            // Perform inline projection for the DMs
            storeOptions.Events.UseAggregatorLookup(AggregationLookupStrategy.UsePublicAndPrivateApply);
        }
    }
}