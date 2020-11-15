using System.ComponentModel.DataAnnotations;

namespace MoE.ECE.Domain.Model
{
    public abstract class DomainEntity
    {
        [Timestamp] public byte[] RowVersion { get; set; } = null!;

        public int Id { get; set; }
    }
}