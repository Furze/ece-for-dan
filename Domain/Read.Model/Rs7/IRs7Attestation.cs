namespace MoE.ECE.Domain.Read.Model.Rs7
{
    public interface IRs7Attestation : IHasId
    {
        public bool? IsAttested { get; set; }
    }
}