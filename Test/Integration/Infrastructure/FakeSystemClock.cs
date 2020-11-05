using Microsoft.Extensions.Internal;
using System;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    public class FakeSystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow => StaticDateTime ?? DateTime.Now.ToUniversalTime();

        public DateTimeOffset? StaticDateTime { get; private set; }    

        public void Reset()
        {
            StaticDateTime = null;
        }

        public void SetStaticDateTime(DateTimeOffset? staticDateTime)
        {
            StaticDateTime = staticDateTime;
        }
    }
}