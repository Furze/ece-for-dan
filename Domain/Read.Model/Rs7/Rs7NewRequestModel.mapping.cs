using AutoMapper;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Infrastructure.Extensions;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class Rs7NewRequestModelMapping : Profile
    {
        public Rs7NewRequestModelMapping()
        {
            CreateMap<CreateRs7, Rs7NewRequestModel>().Ignore(x => x.Rs7Id);
        }
    }
}