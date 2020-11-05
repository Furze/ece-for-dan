using System;
using Microsoft.Extensions.Internal;

namespace MoE.ECE.Domain.Infrastructure
{
    public class SystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow => DateTime.UtcNow;
    }
}