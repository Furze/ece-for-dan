using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    public interface IEventTracker<TEventType>
        where TEventType : class
    {
        ConcurrentDictionary<Type, TEventType> Events { get; }

        void Add(TEventType notification);

        void Reset();

        TDerivedEventType? GetEvent<TDerivedEventType>()
            where TDerivedEventType : class;

        bool ReceivedEvent<TDerivedEventType>()
            where TDerivedEventType : class;

        /// <summary>
        ///     Returns an enumerable list of the names of the received events.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> ReceivedEvents();
    }
}