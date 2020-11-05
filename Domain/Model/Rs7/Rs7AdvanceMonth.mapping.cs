using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Read.Model.Rs7;

namespace MoE.ECE.Domain.Model.Rs7
{
    public class Rs7AdvanceMonthMapping : Profile
    {
        public Rs7AdvanceMonthMapping()
        {
            CreateMap<Rs7AdvanceMonthModel, Rs7AdvanceMonth>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Rs7Revision)
                .Ignore(dest => dest.Rs7RevisionId);
        }
    }
}