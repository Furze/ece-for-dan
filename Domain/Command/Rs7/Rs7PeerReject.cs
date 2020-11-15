using System;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class Rs7PeerReject : ICommand
    {
        public Rs7PeerReject(Guid businessEntityId)
        {
            BusinessEntityId = businessEntityId;
        }

        public Guid BusinessEntityId { get; }
    }
}