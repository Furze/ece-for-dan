namespace MoE.ECE.Domain.Read.Model
{
    public class PaginationModel
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int Count { get; set; }
    }
}