using System;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class DeclineRs7 : ICommand
    {
        public DeclineRs7(Guid businessEntityId) => BusinessEntityId = businessEntityId;

        public Guid BusinessEntityId { get; }
    }
}