using Hangfire.Dashboard;

namespace Sauron.Infrastructure.Hangfire
{
    public class AllowAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context) => true;
    }
}
