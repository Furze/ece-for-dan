using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MoE.ECE.Domain.Command;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Model.ValueObject;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Coordinator
{
    public class Rs7Coordinator : IDomainEventHandler<Rs7Updated>
    {
        private readonly ICqrs _cqrs;
        private readonly IMapper _mapper;

        public Rs7Coordinator(ICqrs cqrs, IMapper mapper)
        {
            _cqrs = cqrs;
            _mapper = mapper;
        }
        public Task Handle(Rs7Updated domainEvent, CancellationToken cancellationToken)
        {
            if (domainEvent.RollStatus == RollStatus.InternalReadyForReview)
            {
                var createRs7 = _mapper.Map<CreateOperationalFundingRequest>(domainEvent);

                return _cqrs.ExecuteAsync(
                    createRs7,
                    cancellationToken);
            }

            //TODO: Maybe look at implementing compensating transaction here...??
            return Task.CompletedTask;
        }
    }
}