using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MoE.ECE.Domain.Command;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Model.ValueObject;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Coordinator
{
    public class Rs7Coordinator : IDomainEventHandler<Rs7Updated>, IDomainEventHandler<Rs7ZeroReturnCreated>,
        IDomainEventHandler<FullRs7Created>
    {
        private readonly ICqrs _cqrs;
        private readonly IMapper _mapper;

        public Rs7Coordinator(ICqrs cqrs, IMapper mapper)
        {
            _cqrs = cqrs;
            _mapper = mapper;
        }

        public Task Handle(FullRs7Created domainEvent, CancellationToken cancellationToken)
        {
            var createRs7 = _mapper.Map<CreateOperationalFundingRequest>(domainEvent);

            return _cqrs.ExecuteAsync(
                createRs7,
                cancellationToken);
        }

        public Task Handle(Rs7Updated domainEvent, CancellationToken cancellationToken)
        {
            if (domainEvent.RollStatus != RollStatus.InternalReadyForReview)
            {
                return Task.CompletedTask;
            }

            var createRs7 = _mapper.Map<CreateOperationalFundingRequest>(domainEvent);

            return _cqrs.ExecuteAsync(
                createRs7,
                cancellationToken);
        }

        public Task Handle(Rs7ZeroReturnCreated domainEvent, CancellationToken cancellationToken)
        {
            var createRs7 = _mapper.Map<CreateOperationalFundingRequest>(domainEvent);

            return _cqrs.ExecuteAsync(
                createRs7,
                cancellationToken);
        }
    }
}