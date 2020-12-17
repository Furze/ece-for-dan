using System;
using System.Collections.Generic;
using System.Linq;
using Moe.ECE.Events.Integration;
using Moe.Library.ServiceBus;

namespace MoE.ECE.Web.Infrastructure.ServiceBus
{
    public class MessageResolver : IMessageResolver
    {
        private readonly List<Type> _integrationEventTypes;

        public MessageResolver() =>
            _integrationEventTypes = typeof(IIntegrationEvent).Assembly.GetTypes().Where(type =>
                typeof(IIntegrationEvent).IsAssignableFrom(type) && type.IsInterface == false).ToList();

        public Type DetermineMessageType(string typeName, MessageFormat messageFormat) =>
            messageFormat == MessageFormat.Json
                ? ResolveJsonMessageType(typeName)
                : ResolveProtoMessageType(typeName);

        private Type ResolveProtoMessageType(string typeName)
        {
            var messageType = _integrationEventTypes.SingleOrDefault(type =>
                string.Equals(type.Name, typeName, StringComparison.CurrentCultureIgnoreCase));

            if (messageType == null)
                throw new NotSupportedException($"{typeName} is not a supported message type");
            
            return messageType;
        }

        private static Type ResolveJsonMessageType(string typeName) =>
            typeName switch
            {
                _ => throw new NotSupportedException($"{typeName} is not a supported message type")
            };
    }
}