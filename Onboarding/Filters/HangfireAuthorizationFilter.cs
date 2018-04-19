using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace Onboarding.Filters
{
    public class HangfireAuthorization : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
