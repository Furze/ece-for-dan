using System;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Event
{
    public class Rs7Discarded : IDomainEvent
    {
        public int Id { get; set; }
        public Guid BusinessEntityId { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}