namespace MoE.ECE.Web.Infrastructure.Authorisation.Resources
{
    public static class Services
    {
        public const string ResourceName = "services";
        
        public const string GetService = ResourceName + ActionNames.View;
        public const string ListServices = ResourceName + ActionNames.List;
    }
}