using Hangfire.Dashboard;

namespace Sauron.Extensions.Hangfire
{
    public class AllowAnonymousAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context) => true;
    }
}
