using AutoMapper;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7EntitlementMonthModelMapping : Profile
    {
        public Rs7EntitlementMonthModelMapping()
        {
            CreateMap<Domain.Model.Rs7.Rs7EntitlementMonth, Rs7EntitlementMonthModel>();

            CreateMap<Events.Integration.Protobuf.Eli.Rs7EntitlementMonth, Rs7EntitlementMonthModel>();
        }
    }
}