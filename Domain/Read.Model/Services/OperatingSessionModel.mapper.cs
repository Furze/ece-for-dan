using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.ReferenceData;

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