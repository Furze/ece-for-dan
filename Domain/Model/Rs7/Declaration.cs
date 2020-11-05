namespace MoE.ECE.Domain.Model.Rs7
{
    public class Declaration : DomainEntity
    {
        public string FullName { get; set; } = string.Empty;

        public string ContactPhone { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public bool? IsDeclaredTrue { get; set; }

        public int Rs7RevisionId { get; set; }

        public virtual Rs7Revision Rs7Revision { get; set; } = null!;

        public Declaration Clone()
        {
            return new Declaration
            {
                FullName = FullName,
                ContactPhone = ContactPhone,
                Role = Role,
                IsDeclaredTrue = IsDeclaredTrue
            };
        }
    }
}