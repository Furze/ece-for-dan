using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.OperationalFunding;

namespace MoE.ECE.Domain.Read.Model.OperationalFunding
{
    public class EntitlementFundingComponentModelMapping : Profile
    {
        public EntitlementFundingComponentModelMapping()
        {
            CreateMap<EntitlementFundingComponent, EntitlementFundingComponentModel>()
                .Map(dest => dest.SessionType, src => src.SessionTypeId)
                .Map(dest => dest.FundingComponentType, src => src.FundingComponentTypeId);
        }
    }
}