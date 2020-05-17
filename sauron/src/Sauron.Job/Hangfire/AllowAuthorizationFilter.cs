using Hangfire.Dashboard;

namespace Sauron.Job.Hangfire
{
    public class AllowAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context) => true;
    }
}
