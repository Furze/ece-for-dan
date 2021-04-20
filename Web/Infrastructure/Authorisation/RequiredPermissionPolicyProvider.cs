using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace MoE.ECE.Web.Infrastructure.Authorisation
{
    public class RequiredPermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public const string HangfirePolicy = "Hangfire";
        private readonly AuthorizationPolicy _defaultAuthorisationPolicy;

        public RequiredPermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            // Store the existing Default policy for any policy fall-through stuff which isn't using
            // the RequirePermission auth policy.
            _defaultAuthorisationPolicy = options.Value.DefaultPolicy;
        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => Task.FromResult(_defaultAuthorisationPolicy);

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => Task.FromResult((AuthorizationPolicy?)_defaultAuthorisationPolicy);

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName == HangfirePolicy)
            {
                // Just require an authenticated user at this stage - can check for a proper 
                // permission later
                var hangfirePolicy = new AuthorizationPolicyBuilder(OpenIdConnectDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser();

                return Task.FromResult((AuthorizationPolicy?)hangfirePolicy.Build());
            }

            // the permission name and grade are extracted from the policy name eg XYZ <= RequirePermission_permissonName_grace
            var items = policyName.Split('_');
            var permission = string.Empty;
            var graceEnabled = true;
            
            if (items.Length > 1)
                permission = policyName.Split('_')[1]; // zero-indexed!
            
            if (items.Length > 2)
                graceEnabled = bool.Parse(policyName.Split('_')[2]); // zero-indexed!
            
            if (string.IsNullOrEmpty(permission))
                return GetFallbackPolicyAsync();
            
            // Create a custom policy for this permission bound to the JwtBearerDefaults.AuthenticationScheme ("Bearer")
            // AuthN scheme, and add the PermissionRequirement requirement to it.
            var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .AddRequirements(new PermissionRequirement(permission, graceEnabled));
            
            // build and return it.
            return Task.FromResult((AuthorizationPolicy?)policy.Build());
        }
    }
}