using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moe.Library.Cqrs;
using Newtonsoft.Json;

namespace MoE.ECE.Web.Infrastructure
{
    public class MediatorCqrs : ICqrs
    {
        private const string Query = "Query";
        private const string QueryResponse = "Query Response";
        private const string DomainEvent = "Domain Event";
        private const string Command = "Command";
        private readonly ILogger<MediatorCqrs> _logger;
        private readonly IMediator _mediator;

        public MediatorCqrs(IMediator mediator, ILogger<MediatorCqrs> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public Task ExecuteAsync<TCommand>(CancellationToken cancellationToken = new())
            where TCommand : ICommand, new() => ExecuteAsync(new TCommand(), cancellationToken);

        public Task ExecuteAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            LogCommand(command);
            return _mediator.Send(command, cancellationToken);
        }

        public Task<int> ExecuteAsync(IBeginSagaCommand command, CancellationToken cancellationToken = default)
        {
            LogCommand(command);
            return _mediator.Send(command, cancellationToken);
        }

        public async Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query,
            CancellationToken cancellationToken = default)
        {
            LogQuery(query);

            TResponse response = await _mediator.Send(query, cancellationToken);

            LogQueryResponse(response);

            return response;
        }

        public async Task RaiseEventsAsync(IEnumerable<IDomainEvent> domainEvents,
            CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                await RaiseEventAsync(domainEvent, cancellationToken);
            }
        }

        public Task RaiseEventAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            LogDomainEvent(domainEvent);
            return _mediator.Publish(domainEvent, cancellationToken);
        }

        private void LogQuery(object query) => Log("[Executing Query]", query, Query);

        private void LogCommand<T>(IRequest<T> command) => Log("[Executing Command]", command, Command);

        private void LogDomainEvent(IDomainEvent domainEvent) => Log("[Domain Event Raised]", domainEvent, DomainEvent);

        private void LogQueryResponse(object? response) => Log("Query Response", response, QueryResponse);

        private void Log(string message, object? objectToLog, string cqrsType)
        {
            if (objectToLog == null)
            {
                return;
            }

            string? formattedMessage = message + " :{objectType} {objectJson} {cqrsType}";
            _logger.LogDebug(formattedMessage, objectToLog.GetType().Name,
                JsonConvert.SerializeObject(objectToLog, Formatting.Indented), cqrsType);
        }
    }
}