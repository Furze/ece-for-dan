namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public interface IRs7AdvanceMonths : IHasId
    {
        public Rs7AdvanceMonthModel[]? AdvanceMonths { get; set; }
    }
}