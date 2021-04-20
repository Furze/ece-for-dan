using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using MoE.ECE.Integration.OpenPolicyAgent;

namespace MoE.ECE.Web.Infrastructure.Authorisation
{
    public class LocalDevelopmentTokenValidator : ISecurityTokenValidator
    {
        public LocalDevelopmentTokenValidator(IWebHostEnvironment webHostEnvironment)
        {
            if (!LocalDevelopment.DisableAuthnAndAuthz(webHostEnvironment))
                throw new SecurityException("Will not disable authn and authz for non local development.");
        }
        
        public bool CanReadToken(string securityToken) => true;
        
        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters,
            out SecurityToken validatedToken)
        {
            validatedToken = new JwtSecurityToken();
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "Bob the Happy Pirate"),
                new Claim(ClaimsIdentity.DefaultNameClaimType, "bob_the_happy_pirate@gmail.com"),
            };

            return new ClaimsPrincipal(new ClaimsIdentity(claims, "Bearer"));
        }

        public bool CanValidateToken => true;
        
        public int MaximumTokenSizeInBytes { get; set; }
    }
}