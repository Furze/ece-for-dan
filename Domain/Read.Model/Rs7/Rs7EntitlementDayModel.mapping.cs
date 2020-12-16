using AutoMapper;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7EntitlementDayModelMapping : Profile
    {
        public Rs7EntitlementDayModelMapping()
        {
            CreateMap<Domain.Model.Rs7.Rs7EntitlementDay, Rs7EntitlementDayModel>();

            CreateMap<Events.Integration.Protobuf.Eli.Rs7EntitlementDay, Rs7EntitlementDayModel>();
        }
    }
}