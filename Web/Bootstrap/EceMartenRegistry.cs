using System;
using Marten;
using Marten.Services.Events;
using Marten.Storage;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Infrastructure;
using MoE.ECE.Domain.Model.Rs7;

namespace MoE.ECE.Web.Bootstrap
{
    public class EceMartenRegistry : MartenRegistry
    {
        public EceMartenRegistry() => For<Rs7>().Identity(rs7 => rs7.Id);

        public static void ApplyDefaultConfiguration(StoreOptions storeOptions, MartenSettings settings,
            IServiceProvider? serviceProvider = null)
        {
            if (string.IsNullOrEmpty(settings.SchemaName) == false)
            {
                storeOptions.DatabaseSchemaName = settings.SchemaName;
            }

            if (string.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new ECEApplicationException("Configuration Error: MartenSettings:ConnectionString not set.");
            }

            storeOptions.Connection(settings.ConnectionString);
            storeOptions.UseDefaultSerialization(nonPublicMembersStorage: NonPublicMembersStorage.NonPublicSetters);

            storeOptions.AutoCreateSchemaObjects = settings.AutoCreate;
            storeOptions.PLV8Enabled = false;
            storeOptions.Policies.ForAllDocuments(_ => _.TenancyStyle = TenancyStyle.Single);

            storeOptions.UseDefaultSerialization(EnumStorage.AsString);

            if (serviceProvider != null)
            {
                TelemetryClient? telemetryClient = serviceProvider.GetService<TelemetryClient>();
                storeOptions.Logger(new AppInsightsMartenLogger(telemetryClient));
            }

            storeOptions.Schema.Include<EceMartenRegistry>();

            EventConfiguration(storeOptions);
        }

        private static void EventConfiguration(StoreOptions storeOptions) =>
            // Perform inline projection for the DMs
            storeOptions.Events.UseAggregatorLookup(AggregationLookupStrategy.UsePublicAndPrivateApply);
    }
}