using AutoMapper;
using MoE.ECE.Domain.Read.Model.Rs7;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class SaveAsDraftMapper : Profile
    {
        public SaveAsDraftMapper()
        {
            CreateMap<Rs7Model, SaveAsDraft>();
        }
    }
}