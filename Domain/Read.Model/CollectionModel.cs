namespace MoE.ECE.Domain.Read.Model
{
    public class CollectionModel<T>
    {
        public CollectionModel()
        {
            Data = new T[0];
            Pagination = new PaginationModel();
        }

        public CollectionModel(int pageSize, int pageNumber, int count, T[] data)
        {
            Pagination = new PaginationModel
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                Count = count
            };

            Data = data;
        }

        public T[] Data { get; set; }

        public PaginationModel Pagination { get; set; }
    }
}