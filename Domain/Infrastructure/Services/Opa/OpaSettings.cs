using System.ComponentModel.DataAnnotations;

namespace MoE.ECE.Domain.Infrastructure.Services.Opa
{
    public class OpaSettings
    {
        [Required]
        public string AuthorisationUrl { get; set; } = string.Empty;

        [Required]
        public string RuleBaseUrl { get; set; } = string.Empty;

        [Required]
        public string UserName { get; set; } = string.Empty;
        
        [Required]
        public string UserSecret { get; set; } = string.Empty;
    }
}
