using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class UpdateRs7Declaration : DeclarationModel, ICommand
    {
        public int Id { get; set; }
    }
}