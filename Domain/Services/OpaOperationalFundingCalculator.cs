using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MoE.ECE.Domain.Infrastructure.Services.Opa;
using MoE.ECE.Domain.Services.Opa.Request;
using MoE.ECE.Domain.Services.Opa.Response;

namespace MoE.ECE.Domain.Services
{
    public class OpaOperationalFundingCalculator : IOperationalFundingCalculator
    {
        private readonly IOpaClient _opaClient;
        private readonly string _ruleBaseUrl;

        public OpaOperationalFundingCalculator(
            IOpaClient opaClient,
            IOptions<OpaSettings> opaSettings)
        {
            _opaClient = opaClient;
            _ruleBaseUrl = opaSettings.Value.RuleBaseUrl;
        }

        public async Task<OpaResponse<OperationalFundingBaseResponse>> CalculateAsync(
            OpaRequest<OperationalFundingBaseRequest> operationalFundingBaseRequest)
        {
            var url = SubstitutePlaceholder(_ruleBaseUrl, "rulebase", "ECEOperationalFunding");

            return await _opaClient.PostRequest<OperationalFundingBaseRequest, OperationalFundingBaseResponse>(
                operationalFundingBaseRequest, url);
        }

        private static string SubstitutePlaceholder(string format, string placeholder, string replaceWith) =>
            format.Replace($"{{{placeholder}}}", replaceWith);
    }
}