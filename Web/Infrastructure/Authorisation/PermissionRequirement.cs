using Microsoft.AspNetCore.Authorization;

namespace MoE.ECE.Web.Infrastructure.Authorisation
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string RequiredPermission { get; }
        /// <summary>
        /// THIS SHOULD BE REMOVED AT SOME POINT AFTER THE DEV "GRACE PERIOD" HAS CONCLUDED
        /// </summary>
        public bool AllowAuthorisationGrace { get; }
        public PermissionRequirement(string requiredPermission, bool allowAuthorisationGrace)
        {
            RequiredPermission = requiredPermission;
            AllowAuthorisationGrace = allowAuthorisationGrace;
        }
    }
}