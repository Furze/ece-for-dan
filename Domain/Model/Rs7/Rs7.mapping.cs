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
                .Map(d => d.CurrentRevision, s => s)
                .Ignore(d => d.Id)
                .Ignore(d => d.RollStatus)
                .Ignore(d => d.ReceivedDate)
                .Ignore(d => d.FundingYear)
                .Ignore(d => d.Revisions)
                .Ignore(d => d.RowVersion);

            CreateMap<Rs7Model, Rs7>()
                .Ignore(d => d.Revisions)
                .Ignore(d => d.RowVersion);
        }
    }
}