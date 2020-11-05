using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class CreateRs7Mapping : Profile
    {
        public CreateRs7Mapping()
        {
            CreateMap<CreateRs7, Model.Rs7.Rs7>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.BusinessEntityId)
                .Ignore(dest => dest.RollStatus)
                .Ignore(dest => dest.ReceivedDate)
                .Ignore(dest => dest.Revisions)
                .Ignore(dest => dest.RowVersion)
                .Ignore(dest => dest.FundingYear);
        }
    }
}