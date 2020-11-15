using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoE.ECE.Web.Infrastructure;
using Moe.Library.Cqrs;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    public class TrackingMediatorCqrs : ICqrs
    {
        private readonly ICqrs _cqrsImplementation;
        private readonly IEventTracker<IDomainEvent> _domainEventTracker;

        public TrackingMediatorCqrs(MediatorCqrs cqrsImplementation, IEventTracker<IDomainEvent> domainEventTracker)
        {
            _cqrsImplementation = cqrsImplementation;
            _domainEventTracker = domainEventTracker;
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
            Task? task = _cqrsImplementation.RaiseEventAsync(domainEvent, cancellationToken);

            _domainEventTracker.Add(domainEvent);

            return task;
        }

        public Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query,
            CancellationToken cancellationToken = default) => _cqrsImplementation.QueryAsync(query, cancellationToken);

        public Task ExecuteAsync<TCommand>(CancellationToken cancellationToken = new CancellationToken())
            where TCommand : ICommand, new() => _cqrsImplementation.ExecuteAsync<TCommand>(cancellationToken);

        public Task ExecuteAsync(ICommand command, CancellationToken cancellationToken = default) =>
            _cqrsImplementation.ExecuteAsync(command, cancellationToken);

        public Task<int> ExecuteAsync(IBeginSagaCommand command, CancellationToken cancellationToken = default) =>
            _cqrsImplementation.ExecuteAsync(command, cancellationToken);
    }
}