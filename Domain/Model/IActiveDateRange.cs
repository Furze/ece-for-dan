using System;

namespace MoE.ECE.Domain.Model
{
    /// <summary>
    ///     Indicates that a given entity has a lifetime component to it.
    /// </summary>
    public interface IActiveDateRange
    {
        DateTimeOffset EffectiveFrom { get; set; }

        DateTimeOffset? EffectiveTo { get; set; }
    }
}