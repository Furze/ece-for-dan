using System;
using Microsoft.Extensions.Internal;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    public class FakeSystemClock : ISystemClock
    {
        public DateTimeOffset? StaticDateTime { get; private set; }
        public DateTimeOffset UtcNow => StaticDateTime ?? DateTime.Now.ToUniversalTime();

        public void Reset() => StaticDateTime = null;

        public void SetStaticDateTime(DateTimeOffset? staticDateTime) => StaticDateTime = staticDateTime;
    }
}