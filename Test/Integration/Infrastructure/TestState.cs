using System;
using System.Dynamic;
using Bard;
using Bard.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Moe.ECE.Events.Integration;
using Moe.Library.Cqrs;
using Shouldly;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    public class TestState<TStoryBook, TStoryData> : ITestState
        where TStoryBook : StoryBook<TStoryData> where TStoryData : class, new()
    {
        private IEventTracker<IDomainEvent>? _domainEventTracker;
        private IEventTracker<IIntegrationEvent>? _integrationEventTracker;
        private LogWriter? _logWriter;
        private IScenario<TStoryBook, TStoryData>? _scenario;
        public TestState()
        {
            HasRun = false;
            Data = new ExpandoObject();
        }

        public bool HasRun { get; private set; }

        public IScenario<TStoryBook, TStoryData> Scenario
        {
            get
            {
                if (_scenario == null)
                    throw new Exception($"Scenario is null. {nameof(Initialize)} method should be called.");

                return _scenario;
            }
        }

        public dynamic Data { get; }

        public void Initialize(IScenario<TStoryBook, TStoryData> scenario, IServiceProvider services,
            LogWriter logWriter)
        {
            _scenario = scenario;
            _logWriter = logWriter;
            _domainEventTracker = services.GetService<IEventTracker<IDomainEvent>>();
            _integrationEventTracker = services.GetService<IEventTracker<IIntegrationEvent>>();

            _domainEventTracker.Reset();
            _integrationEventTracker.Reset();

            HasRun = true;
        }

        public void A_domain_event_should_not_be_fired<TDomainEvent>()
            where TDomainEvent : class, IDomainEvent
        {
            var domainEvent = _domainEventTracker?.GetEvent<TDomainEvent>();

            domainEvent.ShouldBeNull(
                $"A '{typeof(TDomainEvent).Name}'domain event should not have been fired but it was.");
        }

        public TDomainEvent A_domain_event_should_be_fired<TDomainEvent>()
            where TDomainEvent : class, IDomainEvent
        {
            var domainEvent = _domainEventTracker?.GetEvent<TDomainEvent>();

            if (domainEvent == null)
            {
                foreach (var (_, value) in _domainEventTracker!.Events)
                {
                    WriteDomainEventToConsole(value);
                }

                var receivedEvents = _domainEventTracker.ReceivedEvents();
                // ReSharper disable once ExpressionIsAlwaysNull
                domainEvent.ShouldNotBeNull(
                    $"A {typeof(TDomainEvent).Name} domain event was not fired. The following events were fired though. {string.Join(", ", receivedEvents)}");
            }
            else
            {
                WriteDomainEventToConsole(domainEvent);
            }

            return domainEvent!;
        }

        public TIntegrationEvent An_integration_event_should_be_fired<TIntegrationEvent>()
            where TIntegrationEvent : class, IIntegrationEvent
        {
            var domainEvent = _integrationEventTracker?.GetEvent<TIntegrationEvent>();

            if (domainEvent == null)
            {
                foreach (var (_, value) in _integrationEventTracker!.Events)
                {
                    WriteIntegrationEventToConsole(value);
                }

                var receivedEvents = _integrationEventTracker.ReceivedEvents();
                // ReSharper disable once ExpressionIsAlwaysNull
                domainEvent.ShouldNotBeNull(
                    $"A {typeof(TIntegrationEvent).Name} integration event was not fired. The following events were fired though. {string.Join(", ", receivedEvents)}");
            }
            else
            {
                WriteIntegrationEventToConsole(domainEvent);
            }

            return domainEvent!;
        }

        private void WriteIntegrationEventToConsole<TIntegrationEvent>(TIntegrationEvent integrationEvent)
            where TIntegrationEvent : class, IIntegrationEvent
        {
            WriteEventToConsole(integrationEvent, "Integration");
        }

        private void WriteDomainEventToConsole<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : class, IDomainEvent
        {
            WriteEventToConsole(domainEvent, "Domain");
        }

        private void WriteEventToConsole<TDomainEv>(TDomainEv dEvent, string eventType)
        {
            _logWriter?.LogHeaderMessage($"{eventType} Event - {dEvent?.GetType().Name}");
            _logWriter?.LogObject(dEvent);
        }
    }
}