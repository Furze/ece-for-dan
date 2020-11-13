using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class DiscardRs7 : ICommand
    {
        public int Id { get; set; }
    }
}