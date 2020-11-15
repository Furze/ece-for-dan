using AutoMapper;
using MoE.ECE.Domain.Model.ReferenceData;

namespace MoE.ECE.Domain.Read.Model.Services
{
    public class SearchEceServiceModelMapper : Profile
    {
        public SearchEceServiceModelMapper()
        {
            CreateMap<EceService, SearchEceServiceModel>();
        }
    }
}