using System;
using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Query
{
    public class GetRs7ByBusinessEntityId : IQuery<Rs7Model>
    {
        public GetRs7ByBusinessEntityId(Guid businessEntityId) => BusinessEntityId = businessEntityId;

        public Guid BusinessEntityId { get; }
    }
}