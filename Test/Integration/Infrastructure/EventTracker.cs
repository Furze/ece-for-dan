using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    /// <summary>
    ///     Keeps track of various different events flowing through the system. Domain Events & Integration Events
    ///     so we can verify in our integration tests that they have been fired.
    /// </summary>
    public class EventTracker<TEventType> : IEventTracker<TEventType> where TEventType : class
    {
        private readonly string _domainEventCache = $"{typeof(TEventType).Name}_cache";
        private readonly IMemoryCache _memoryCache;

        public EventTracker(IMemoryCache memoryCache) => _memoryCache = memoryCache;

        public ConcurrentDictionary<Type, TEventType> Events => _memoryCache.GetOrCreate(
            _domainEventCache,
            entry => new ConcurrentDictionary<Type, TEventType>());

        public void Add(TEventType notification)
        {
            if (Events.ContainsKey(notification.GetType()))
            {
                Events[notification.GetType()] = notification;
            }
            else
            {
                Events.TryAdd(notification.GetType(), notification);
            }
        }

        public void Reset() => Events.Clear();

        public TDerivedEventType? GetEvent<TDerivedEventType>()
            where TDerivedEventType : class
        {
            if (ReceivedEvent<TDerivedEventType>())
            {
                return Events[typeof(TDerivedEventType)] as TDerivedEventType;
            }

            return null;
        }

        public bool ReceivedEvent<TDerivedEventType>()
            where TDerivedEventType : class =>
            Events.ContainsKey(typeof(TDerivedEventType));

        /// <summary>
        ///     Returns an enumerable list of the names of the received events.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> ReceivedEvents() => Events.Select(pair => pair.Key.Name);
    }
}