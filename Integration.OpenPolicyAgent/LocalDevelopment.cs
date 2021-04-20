using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MoE.ECE.Integration.OpenPolicyAgent
{
    public static class LocalDevelopment
    {
        public static bool DisableAuthnAndAuthz(IWebHostEnvironment? hostEnvironment)
        {
#if DEBUG
            if (hostEnvironment.IsDevelopment())
                return true;

            return false;
#else
            return false;
#endif
        }
    }
}