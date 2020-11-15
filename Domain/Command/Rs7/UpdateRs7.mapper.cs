using AutoMapper;
using MoE.ECE.Domain.Read.Model.Rs7;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class UpdateRs7Mapper : Profile
    {
        public UpdateRs7Mapper()
        {
            CreateMap<Rs7Model, UpdateRs7>();
        }
    }
}