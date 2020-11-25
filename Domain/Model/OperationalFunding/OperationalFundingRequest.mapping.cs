using AutoMapper;
using MoE.ECE.Domain.Services.Opa.Mappings.Converters;
using MoE.ECE.Domain.Services.Opa.Response;

namespace MoE.ECE.Domain.Model.OperationalFunding
{
    public class OperationalFundingRequestMapping : Profile
    {
        public OperationalFundingRequestMapping() =>
            CreateMap<OperationalFundingBaseResponse, OperationalFundingRequest>()
                .ConvertUsing<OpaToOperationalFundingConverter>();
    }
}