using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MoE.ECE.Web.Infrastructure.Authorisation
{
    /// <summary>
    ///     This class will be replaced at some point in the near future with
    ///     the equivalent attribute from Soliance's Policy Server libraries.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RequirePermissionAttribute : Attribute, IAuthorizationFilter
    {
        public const string ForObsoletedAction = "obsolete";

        public RequirePermissionAttribute(string permission) => Permission = permission;

        public string Permission { get; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // for now just return - eg everything is authorised. 
        }
    }
}