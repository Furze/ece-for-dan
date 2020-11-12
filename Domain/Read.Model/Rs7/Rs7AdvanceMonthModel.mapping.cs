using AutoMapper;
using MoE.ECE.Domain.Model.Rs7;
using Moe.ECE.Events.Integration.ELI;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7AdvanceMonthModelMapping : Profile
    {
        public Rs7AdvanceMonthModelMapping()
        {
            CreateMap<Rs7AdvanceMonth, Rs7AdvanceMonthModel>();

            CreateMap<Rs7ReceivedAdvanceMonth, Rs7AdvanceMonthModel>()
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.FundingPeriodYear));
        }
    }
}