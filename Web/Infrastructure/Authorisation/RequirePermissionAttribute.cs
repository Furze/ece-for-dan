using Microsoft.AspNetCore.Authorization;

namespace MoE.ECE.Web.Infrastructure.Authorisation
{
    public sealed class RequirePermissionAttribute : AuthorizeAttribute
    {
        public string Permission { get; }

        /// <summary>
        /// THIS SHOULD BE REMOVED AT SOME POINT AFTER THE DEV "GRACE PERIOD" HAS CONCLUDED
        /// </summary>
        public bool AllowAuthorisationGrace { get; }

        public RequirePermissionAttribute(string permission, bool allowAuthorisationGrace = true)
        {
            Permission = permission;
            AllowAuthorisationGrace = allowAuthorisationGrace; // REMOVE once grace period has ended.
            Policy = $"{PolicyNames.RequirePermissionPolicy}_{permission}_{allowAuthorisationGrace}";
        }
    }
}