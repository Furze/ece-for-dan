using System;
using MoE.ECE.Domain.Model.Rs7;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Event
{
    public class Rs7Discarded : IDomainEvent
    {
        public Rs7Discarded()
        {
        }

        public Rs7Discarded(Rs7 rs7, DateTimeOffset now)
        {
            Id = rs7.Id;
            BusinessEntityId = rs7.BusinessEntityId;
            Time = now;
        }

        public int Id { get; set; }
        public Guid BusinessEntityId { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}