using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marten;
using MediatR;
using Microsoft.Extensions.Internal;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Model.Rs7;
using MoE.ECE.Domain.Model.ValueObject;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Saga
{
     public class Rs7WorkflowSaga : IHandleACommand<ApproveRs7>,
        IHandleACommand<PeerApproveRs7>,
        IHandleACommand<SubmitRs7ForApproval>,
        IHandleACommand<Rs7PeerReject>,
        IHandleACommand<DeclineRs7>
    {
        private readonly IDocumentSession _documentSession;
        private readonly ICqrs _cqrs;
        private readonly IMapper _mapper;
        private readonly ISystemClock _systemClock;

        public Rs7WorkflowSaga(
           IDocumentSession documentSession,
            ICqrs cqrs,
            IMapper mapper,
            ISystemClock systemClock)
        {
            _documentSession = documentSession;
            _cqrs = cqrs;
            _mapper = mapper;
            _systemClock = systemClock;
        }

        public Task<Unit> Handle(ApproveRs7 command, CancellationToken cancellationToken)
        {
            return ApplyStateChange<Rs7Approved>(command.BusinessEntityId, rs7 => rs7.ApproveInternally(_systemClock),
                cancellationToken);
        }

        public Task<Unit> Handle(DeclineRs7 command, CancellationToken cancellationToken)
        {
            return ApplyStateChange<Rs7Declined>(command.BusinessEntityId, rs7 => rs7.Decline(_systemClock),
                cancellationToken);
        }

        public Task<Unit> Handle(PeerApproveRs7 command, CancellationToken cancellationToken)
        {
            return ApplyStateChange<Rs7PeerApproved>(command.BusinessEntityId, rs7 => rs7.PeerApprove(_systemClock),
                cancellationToken);
        }

        public Task<Unit> Handle(Rs7PeerReject command, CancellationToken cancellationToken)
        {
            return ApplyStateChange<Rs7PeerRejected>(command.BusinessEntityId, rs7 => rs7.PeerReject(_systemClock),
                cancellationToken);
        }

        public async Task<Unit> Handle(SubmitRs7ForApproval command, CancellationToken cancellationToken)
        {
            var rs7 = await _documentSession.LoadAsync<Rs7>(command.Id, cancellationToken);

            if (rs7 == null)
                throw DomainExceptions.ResourceNotFoundException<Rs7>(command.Id);
            
            if (rs7.RollStatus != RollStatus.ExternalDraft && rs7.RollStatus != RollStatus.ExternalNew &&
                rs7.RollStatus != RollStatus.ExternalReturnedForEdit)
                throw DomainExceptions.InvalidRollStatusForSubmitRs7ForApproval(rs7.RollStatus);

            _mapper.Map(command, rs7.CurrentRevision);

            rs7.RollStatus = RollStatus.ExternalSubmittedForApproval;
            rs7.CurrentRevision.UpdateRevisionDate(_systemClock);

            _documentSession.Update(rs7);
            await _documentSession.SaveChangesAsync(cancellationToken);

            var domainEvent = _mapper.Map<Rs7SubmittedForApproval>(rs7);

            await _cqrs.RaiseEventAsync(domainEvent, cancellationToken);

            return Unit.Value;
        }

        private async Task<Unit> ApplyStateChange<TDomainEvent>(Guid businessEntityId,
            Action<Rs7> changeState, CancellationToken cancellationToken) where TDomainEvent : IDomainEvent
        {
            var rs7 = await _documentSession.Query<Rs7>()
                .SingleOrDefaultAsync(entity => entity.BusinessEntityId == businessEntityId, cancellationToken);
               
            if (rs7 == null)
                throw DomainExceptions.ResourceNotFoundException<Rs7>(businessEntityId);

            changeState(rs7);

            _documentSession.Update(rs7);
            
            await _documentSession.SaveChangesAsync(cancellationToken);

            var domainEvent = _mapper.Map<TDomainEvent>(rs7);

            await _cqrs.RaiseEventAsync(domainEvent, cancellationToken);

            return Unit.Value;
        }
    }
}