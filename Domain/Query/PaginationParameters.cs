namespace MoE.ECE.Domain.Query
{
    public class PaginationParameters
    {
        public int PageSize { get; set; } = 10;

        public int PageNumber { get; set; } = 1;
    }
}