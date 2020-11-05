using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.Rs7;
using Moe.ECE.Events.Integration.ELI;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7EntitlementMonthModelMapping : Profile
    {
        public Rs7EntitlementMonthModelMapping()
        {
            CreateMap<Rs7EntitlementMonth, Rs7EntitlementMonthModel>();

            CreateMap<Rs7ReceivedEntitlementMonth, Rs7EntitlementMonthModel>()
                .Ignore(dest => dest.Id)
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.FundingPeriodYear));
        }
    }
}