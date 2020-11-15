using System;
using System.Runtime.CompilerServices;
using Serilog.Core;
using Serilog.Events;

namespace MoE.ECE.Web.Infrastructure.Middleware.Logging
{
    public class VersionTagEnricher : ILogEventEnricher
    {    
        private LogEventProperty? _cachedProperty;
        private const string PropertyName = "VersionTag";
        
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(GetLogEventProperty(propertyFactory));
        }
        
        private LogEventProperty GetLogEventProperty(ILogEventPropertyFactory propertyFactory)
        {
            // Don't care about thread-safety, in the worst case the field gets overwritten and one property will be GCed
            if (_cachedProperty == null)
                _cachedProperty = CreateProperty(propertyFactory);

            return _cachedProperty;
        }

        // Qualify as uncommon-path
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static LogEventProperty CreateProperty(ILogEventPropertyFactory propertyFactory)
        {
            var value = Environment.GetEnvironmentVariable("VERSION_TAG") ?? "NoVersion";
            return propertyFactory.CreateProperty(PropertyName, value);
        }
    }
}