using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Query
{
    public class GetRs7ByIdRevisionNumber : IQuery<Rs7Model>
    {
        public GetRs7ByIdRevisionNumber(int id, int? revisionNumber)
        {
            Id = id;
            RevisionNumber = revisionNumber;
        }

        public int Id { get; }

        public int? RevisionNumber { get; }
    }
}