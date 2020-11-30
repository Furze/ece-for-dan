using System.Linq;
using Marten.Pagination;

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
        
        public CollectionModel(IPagedList<T> data)
        {
            Pagination = new PaginationModel
            {
                PageSize = data.PageSize,
                PageNumber = data.PageNumber,
                Count = data.TotalItemCount
            };

            Data = data.ToArray();
        }

        public T[] Data { get; set; }

        public PaginationModel Pagination { get; set; }
    }
}