using AutoMapper;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Read.Model.Rs7;

namespace MoE.ECE.Domain.Model.Rs7
{
    public class Rs7Mapping : Profile
    {
        public Rs7Mapping()
        {
            CreateMap<CreateRs7FromExternal, Rs7>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Revisions)
                .Ignore(dest => dest.RowVersion);

            CreateMap<Rs7Model, Rs7>()
                .Ignore(dest => dest.Revisions)
                .Ignore(dest => dest.RowVersion);
        }
    }
}