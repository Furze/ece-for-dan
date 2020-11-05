using System.ComponentModel;

namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public class DeclarationModel
    {
        [ReadOnly(true)]
        public int Id { get; set; }

        public string? FullName { get; set; }

        public string? ContactPhone { get; set; }

        public string? Role { get; set; }

        public bool? IsDeclaredTrue { get; set; }
    }
}