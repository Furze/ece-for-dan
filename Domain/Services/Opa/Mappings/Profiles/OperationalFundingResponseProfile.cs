using AutoMapper;
using MoE.ECE.Domain.Model.OperationalFunding;
using MoE.ECE.Domain.Services.Opa.Mappings.Converters;
using MoE.ECE.Domain.Services.Opa.Response;

namespace MoE.ECE.Domain.Services.Opa.Mappings.Profiles
{
    public class OperationalFundingResponseProfile : Profile
    {
        public OperationalFundingResponseProfile()
        {
            CreateMap<OperationalFundingBaseResponse, FundingRequest>()
                .ConvertUsing<OpaToOperationalFundingConverter>();
        }
    }
}
