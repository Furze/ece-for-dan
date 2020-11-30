namespace MoE.ECE.Domain.Read.Model
{
    public class PaginationModel
    {
        public long PageSize { get; set; }

        public long PageNumber { get; set; }

        public long Count { get; set; }
    }
}