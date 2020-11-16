using System.Threading.Tasks;
using MoE.ECE.Domain.Infrastructure.Services.Opa;
using MoE.ECE.Domain.Services.Opa.Request;
using MoE.ECE.Domain.Services.Opa.Response;

namespace MoE.ECE.Domain.Services
{
    public interface IOperationalFundingCalculator
    {
        Task<OpaResponse<OperationalFundingBaseResponse>> CalculateAsync(OpaRequest<OperationalFundingBaseRequest> request);
    }
}