using System.Threading;
using Bard;
using Marten;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.Domain.Infrastructure;
using Moe.ECE.Events.Integration;
using MoE.ECE.Integration.Tests.Infrastructure;
using Moe.Library.Cqrs;
using Shouldly;

namespace MoE.ECE.Integration.Tests.Chapter
{
    public static class ContextExtensions
    {
        public static T GetService<T>(this ScenarioContext context) => context.Services.GetService<T>();

        public static IDocumentSession GetDocumentSession(this ScenarioContext context) =>
            context.GetService<IDocumentSession>();

        /// <summary>
        ///     Retrieves the domain event from the tracker and logs it to the output window.
        /// </summary>
        /// <param name="context"></param>
        /// <typeparam name="TDomainEvent"></typeparam>
        /// <returns></returns>
        /// <exception cref="ShouldAssertException"></exception>
        public static TDomainEvent GetDomainEvent<TDomainEvent>(this ScenarioContext context)
            where TDomainEvent : class
        {
            IEventTracker<IDomainEvent>? domainEventTracker = context.GetService<IEventTracker<IDomainEvent>>();

            TDomainEvent? domainEvent = domainEventTracker.GetEvent<TDomainEvent>();

            if (domainEvent == null)
            {
                throw new ShouldAssertException($"Domain event of type {typeof(TDomainEvent).Name} could not be found");
            }

            context.Writer.LogMessage($"[DomainEvent::{typeof(TDomainEvent).Name}]");
            context.Writer.LogObject(domainEvent);

            return domainEvent;
        }

        public static void CqrsExecute(this ScenarioContext context, IBeginSagaCommand command)
        {
            ICqrs? cqrs = context.GetService<ICqrs>();

            context.Writer.LogMessage($"[Command::{command.GetType().Name}]");
            context.Writer.LogObject(command);

            AsyncHelper.RunSync(() => cqrs.ExecuteAsync(command));
        }

        public static void CqrsExecute(this ScenarioContext context, ICommand command)
        {
            ICqrs? cqrs = context.GetService<ICqrs>();

            context.Writer.LogMessage($"[Command::{command.GetType().Name}]");
            context.Writer.LogObject(command);

            AsyncHelper.RunSync(() => cqrs.ExecuteAsync(command));
        }

        public static void PublishIntegrationEvent<TIntegrationEvent>(this ScenarioContext context,
            TIntegrationEvent integrationEvent) where TIntegrationEvent : IIntegrationEvent
        {
            IMediator? serviceBus = context.GetService<IMediator>();

            context.Writer.LogMessage($"[IntegrationEvent::{typeof(TIntegrationEvent).Name}]");
            context.Writer.LogObject(integrationEvent);
            AsyncHelper.RunSync(() => serviceBus.Publish(integrationEvent, CancellationToken.None));
        }
    }
}