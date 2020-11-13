using System;

namespace MoE.ECE.Domain.Model
{
    public abstract class BusinessEntity : DomainEntity
    {
        public Guid BusinessEntityId { get; set; } = Guid.NewGuid();
    }
}