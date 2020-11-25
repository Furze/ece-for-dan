using System.Threading.Tasks;

namespace MoE.ECE.Domain.Infrastructure.Services.Opa
{
    public interface IOpaClient
    {
        Task<OpaResponse<TResponse>> PostRequest<TRequest, TResponse>(OpaRequest<TRequest> request, string endpointUrl)
            where TResponse : class;
    }
}