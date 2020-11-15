using Newtonsoft.Json;

namespace MoE.ECE.Domain.Read.Model.Services
{
    public class SearchEceServiceModel
    {
        // TODO:
        // At some point we should maybe change ServiceName to OrganisationName so we 
        // can be consistent across schools and ECEs (would need to change the 
        // front-end search code to match of course).
        public int OrganisationId { get; set; }
        public string OrganisationNumber { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public string? ServiceProviderNumber { get; set; }

        [JsonIgnore]
        public string SanitisedOrganisationName { get; set; } = null!;
    }
}
