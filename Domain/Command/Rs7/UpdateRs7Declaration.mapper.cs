using AutoMapper;
using MoE.ECE.Domain.Model.Rs7;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class UpdateRs7DeclarationMapper : Profile
    {
        public UpdateRs7DeclarationMapper()
        {
            CreateMap<UpdateRs7Declaration, Declaration>();
        }
    }
}