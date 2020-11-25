using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.OperationalFunding;

namespace MoE.ECE.Domain.Read.Model.OperationalFunding
{
    public class AdvanceMonthFundingComponentModelMapping : Profile
    {
        public AdvanceMonthFundingComponentModelMapping()
        {
            CreateMap<AdvanceMonthFundingComponent, AdvanceMonthFundingComponentModel>()
                .Map(dest => dest.FundingComponents, src => src.AdvanceFundingComponents);
        }
    }
}