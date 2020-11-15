using System;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class ApproveRs7 : ICommand
    {
        public ApproveRs7(Guid businessEntityId) => BusinessEntityId = businessEntityId;

        public Guid BusinessEntityId { get; }
    }
}