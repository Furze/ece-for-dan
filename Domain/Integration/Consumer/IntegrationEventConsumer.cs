using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Events.Integration.Protobuf.Workflow;
using Marten;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Model.Rs7;
using MoE.ECE.Domain.Model.ValueObject;
using Moe.ECE.Events.Integration;
using Moe.ECE.Events.Integration.ELI;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Integration.Consumer
{
    public class IntegrationEventConsumer : IIntegrationEventHandler<Approved>,
        IIntegrationEventHandler<Rs7Received>,
        IIntegrationEventHandler<Returned>,
        IIntegrationEventHandler<Declined>
    {
        private readonly ICqrs _cqrs;
        private readonly IDocumentSession _documentSession;
        private readonly IMapper _mapper;

        public IntegrationEventConsumer(
            ICqrs cqrs,
            IDocumentSession documentSession,
            IMapper mapper)
        {
            _cqrs = cqrs;
            _documentSession = documentSession;
            _mapper = mapper;
        }

        public async Task Handle(Approved workflowApproved, CancellationToken cancellationToken)
        {
            if (workflowApproved.BusinessEntityType == Constants.BusinessEntityTypes.Rs7)
            {
                var rs7 = await _documentSession.Query<Rs7>()
                    .SingleOrDefaultAsync(document => document.BusinessEntityId == workflowApproved.BusinessEntityId,
                        cancellationToken);

                if (rs7.RollStatus == RollStatus.ExternalSubmittedForApproval)
                {
                    var command = new PeerApproveRs7(workflowApproved.BusinessEntityId);

                    await _cqrs.ExecuteAsync(command, cancellationToken);
                }
                else
                {
                    var command = new ApproveRs7(workflowApproved.BusinessEntityId);

                    await _cqrs.ExecuteAsync(command, cancellationToken);
                }
            }
            else
            {
                throw new InvalidOperationException(
                    $"We have not implemented a handler for the business entity type - {workflowApproved.BusinessEntityType}");
            }
        }

        public Task Handle(Declined integrationEvent, CancellationToken cancellationToken)
        {
            if (integrationEvent.BusinessEntityType != Constants.BusinessEntityTypes.Rs7)
                throw new InvalidOperationException(
                    $"We have not implemented a handler for the business entity type - {integrationEvent.BusinessEntityType}");

            var command = new DeclineRs7(integrationEvent.BusinessEntityId);

            return _cqrs.ExecuteAsync(command, cancellationToken);
        }

        public async Task Handle(Returned integrationEvent, CancellationToken cancellationToken)
        {
            if (integrationEvent.BusinessEntityType == Constants.BusinessEntityTypes.Rs7)
            {
                var command = new Rs7PeerReject(integrationEvent.BusinessEntityId);

                await _cqrs.ExecuteAsync(command, cancellationToken);
            }
            else
            {
                throw new InvalidOperationException(
                    $"We have not implemented a handler for the business entity type - {integrationEvent.BusinessEntityType}");
            }
        }

        public Task Handle(Rs7Received rs7Received, CancellationToken cancellationToken)
        {
            var createRs7 = _mapper.Map<CreateRs7FromExternal>(rs7Received);

            return _cqrs.ExecuteAsync(createRs7, cancellationToken);
        }
    }
}