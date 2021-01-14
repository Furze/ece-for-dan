namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceServiceDateRangedParameter : DateRangedParameterBase
    {
        public virtual EceService EceService { get; set; } = null!;
    }
}
