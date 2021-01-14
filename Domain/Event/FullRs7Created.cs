using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Event
{
    /// <summary>
    /// Domain event to indicate that an Rs7 return was created, not via data submitted
    /// directly to the ECE API, but rather via an external SMS system.
    /// </summary>
    public class FullRs7Created : Rs7Model, IDomainEvent
    {
        public int RevisionId { get; set; }
        public string? Source { get; set; }
    }
}