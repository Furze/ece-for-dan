using AutoMapper;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7AdvanceMonthModelMapping : Profile
    {
        public Rs7AdvanceMonthModelMapping()
        {
            CreateMap<Domain.Model.Rs7.Rs7AdvanceMonth, Rs7AdvanceMonthModel>();

            CreateMap<Events.Integration.Protobuf.Eli.Rs7AdvanceMonth, Rs7AdvanceMonthModel>();
        }
    }
}