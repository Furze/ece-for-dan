using System;
using Moe.Library.ServiceBus;

namespace MoE.ECE.Web.Infrastructure.ServiceBus
{
    public class MessageResolver : IMessageResolver
    {
        public Type DetermineMessageType(string typeName, MessageFormat messageFormat) =>
            messageFormat == MessageFormat.Json
                ? ResolveJsonMessageType(typeName)
                : ResolveProtoMessageType(typeName);

        private static Type ResolveProtoMessageType(string typeName) =>
            typeName switch
            {
                _ => throw new NotSupportedException($"{typeName} is not a supported message type")
            };

        private static Type ResolveJsonMessageType(string typeName) =>
            typeName switch
            {
                _ => throw new NotSupportedException($"{typeName} is not a supported message type")
            };
    }
}