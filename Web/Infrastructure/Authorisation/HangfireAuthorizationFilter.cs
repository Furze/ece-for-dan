using Hangfire.Dashboard;
using Serilog;

namespace MoE.ECE.Web.Infrastructure.Authorisation
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            Log.Information($"Checking hangfire auth, is authenticated?: {context.GetHttpContext().User.Identity.IsAuthenticated}");
            // At this stage just need to be authenticated - will add a role later
            return context.GetHttpContext().User.Identity.IsAuthenticated;
        }
    }
}