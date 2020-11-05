using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Read.Model.Rs7;

namespace MoE.ECE.Domain.Model.Rs7
{
    public class Rs7EntitlementDayMapping : Profile
    {
        public Rs7EntitlementDayMapping()
        {
            CreateMap<Rs7EntitlementDayModel, Rs7EntitlementDay>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Rs7EntitlementMonth)
                .Ignore(dest => dest.Rs7EntitlementMonthId);
        }
    }
}