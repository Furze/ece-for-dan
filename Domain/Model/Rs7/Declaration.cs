namespace MoE.ECE.Domain.Model.Rs7
{
    public class Declaration
    {
        public string FullName { get; set; } = string.Empty;

        public string ContactPhone { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public bool? IsDeclaredTrue { get; set; }

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