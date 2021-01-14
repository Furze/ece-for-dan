namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceServiceProviderDateRangedParameter : DateRangedParameterBase
    {
        public virtual EceServiceProvider EceServiceProvider { get; set; } = null!;
    }
}
