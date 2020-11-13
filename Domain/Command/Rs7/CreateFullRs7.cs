using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class CreateFullRs7 : Rs7Model, ICommand
    {
        public string Source { get; set; } = string.Empty;
    }
}