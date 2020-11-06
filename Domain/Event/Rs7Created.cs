using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Event
{
    public class Rs7Created : Rs7Model, IDomainEvent
    {
        public string Source { get; set; } = string.Empty;
    }
}