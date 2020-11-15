using AutoMapper;

namespace MoE.ECE.Domain.Read.Model.Services
{
    public class OperatingSessionModelMapper : Profile
    {
        public OperatingSessionModelMapper()
        {
            CreateMap<EceOperatingSession, OperatingSessionModel>()
                .Map(d => d.SessionType, s => s.SessionTypeId);
        }
    }
}