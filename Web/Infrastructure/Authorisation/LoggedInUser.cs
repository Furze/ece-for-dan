using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using MoE.ECE.Domain.Infrastructure.Abstractions;

namespace MoE.ECE.Web.Infrastructure.Authorisation
{
    public class LoggedOnUser : ILoggedOnUser
    {
        private readonly ClaimsIdentity? _claimsIdentity;
        private readonly IIdentity? _identity;

        public LoggedOnUser(IHttpContextAccessor context)
        {
            _identity = context.HttpContext?.User.Identity;
            _claimsIdentity = context.HttpContext?.User.Identities.FirstOrDefault();
        }

        public bool IsAuthenticated => _identity != null && _identity.IsAuthenticated;

        public string UserName => ResolveClaim(JwtClaimTypes.Name);

        private string ResolveClaim(string claimType) =>
            _claimsIdentity?.Claims.FirstOrDefault(claim => claim.Type == claimType)?.Value ?? string.Empty;
    }
}