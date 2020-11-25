using AutoMapper;
using MoE.ECE.Domain.Command;
using MoE.ECE.Domain.Infrastructure.Services.Opa;
using MoE.ECE.Domain.Services.Opa.Mappings.Converters;
using MoE.ECE.Domain.Services.Opa.Request;

namespace MoE.ECE.Domain.Services.Opa.Mappings.Profiles
{
    public class OpaProfile : Profile
    {
        public OpaProfile()
        {
            CreateMap<CreateOperationalFundingRequest, OpaRequest<OperationalFundingBaseRequest>>()
                .ConvertUsing<OperationalFundingToOpaConverter>();
        }
    }
}