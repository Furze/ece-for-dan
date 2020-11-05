using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace MoE.ECE.Web.Infrastructure.Authorisation
{
    public class DisableAuthenticationPolicyEvaluator : IPolicyEvaluator
    {
        public async Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
#if !DEBUG
            throw new System.ApplicationException($"{nameof(DisableAuthenticationPolicyEvaluator)} should not be in use outside of local development.");
            // Always pass authentication.
#else
            var authenticationTicket = new AuthenticationTicket(new ClaimsPrincipal(), new AuthenticationProperties(), JwtBearerDefaults.AuthenticationScheme);
            return await Task.FromResult(AuthenticateResult.Success(authenticationTicket));
#endif
        }

        public async Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object resource)
        {
#if !DEBUG
            throw new System.ApplicationException($"{nameof(DisableAuthenticationPolicyEvaluator)} should not be in use outside of local development.");
#else
            // Always pass authorization
            return await Task.FromResult(PolicyAuthorizationResult.Success());
#endif
        }
    }
}