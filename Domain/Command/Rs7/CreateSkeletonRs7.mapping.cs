using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class CreateSkeletonRs7Mapping : Profile
    {
        public CreateSkeletonRs7Mapping()
        {
            CreateMap<CreateSkeletonRs7, Model.Rs7.Rs7>()
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