using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MoE.ECE.Integration.OpenPolicyAgent;

namespace MoE.ECE.Web.Infrastructure.Authorisation
{
    public class RequiredPermissionAuthorisationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<RequiredPermissionAuthorisationHandler> _logger;
        private readonly IOpenPolicyAgentService _openPolicyAgentService;

        public RequiredPermissionAuthorisationHandler(
            IOpenPolicyAgentService openPolicyAgentService,
            IHttpContextAccessor httpContextAccessor,
            ILogger<RequiredPermissionAuthorisationHandler> logger)
        {
            _openPolicyAgentService = openPolicyAgentService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if(_httpContextAccessor.HttpContext != null)
            {
                var httpContextRequest = _httpContextAccessor.HttpContext.Request;
                if (await _openPolicyAgentService.IsPermittedAsync(requirement.RequiredPermission, httpContextRequest))
                {
                    _logger.LogInformation(
                        "AUTHZ_AUDIT: Authorisation Succeeded for Path {Path}. Permission {RequiredPermission} is assigned to user {Name}.",
                        httpContextRequest.Path,
                        requirement.RequiredPermission,
                        context.User.Identity?.Name);

                    context.Succeed(requirement);
                }
                else
                {
                    /////////////////////////////////////////////////////////////////////////////////
                    // THIS SHOULD BE REMOVED AT SOME POINT AFTER THE DEV "GRACE PERIOD" HAS CONCLUDED
                    if (requirement.AllowAuthorisationGrace)
                    {
                        _logger.LogWarning(
                            $"AUTHZ_AUDIT: Authorisation would have FAILED for Path {httpContextRequest.Path}. " +
                            $"User {context.User.Identity?.Name} does not have Permission {requirement.RequiredPermission}. " +
                            "Allowing access under Authorisation grace period. ");

                        context.Succeed(requirement);
                        return;
                    }
                    /////////////////////////////////////////////////////////////////////////////////

                    _logger.LogWarning(
                        "AUTHZ_AUDIT: Authorisation FAILED for Path {Path}. User {Name} does not have Permission {RequiredPermission}.",
                        httpContextRequest.Path,
                        context.User.Identity?.Name,
                        requirement.RequiredPermission);

                    context.Fail();
                }
            }
        }
    }
}