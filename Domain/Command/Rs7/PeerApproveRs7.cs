using System;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class PeerApproveRs7 : ICommand
    {
        public PeerApproveRs7(Guid businessEntityId) => BusinessEntityId = businessEntityId;

        public Guid BusinessEntityId { get; }
    }
}