using MoE.ECE.Domain.Read.Model.Services;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Query
{
    public class GetServiceById : IQuery<ECEServiceModel>
    {
        public int ServiceId { get; }

        public GetServiceById(int serviceId)
        {
            ServiceId = serviceId;
        }
    }
}