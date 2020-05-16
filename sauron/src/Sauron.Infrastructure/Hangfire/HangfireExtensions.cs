using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sauron.Domain.Jobs;

namespace Sauron.Infrastructure.Hangfire
{
    public static class HangfireExtensions
    {
        public static void AddHangFireHandler(this IServiceCollection services)
        {
            services.AddHangfire(config =>
            {
                config
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseDefaultTypeSerializer()
                    .UseMemoryStorage();
            });

            services.AddHangfireServer();
        }

        public static void UsingHangFireHandler(this IApplicationBuilder app, string pathMatch = "/dashboard")
        {
            app.UseHangfireServer();

            app.UseHangfireDashboard(pathMatch, new DashboardOptions
            {
                Authorization = new[] { new AllowAuthorizationFilter() }
            });
        }

        public static void UseRunJobEverydayAtMidnight<T>(this IApplicationBuilder app) where T : IJob
        {
            var jobManager = app.ApplicationServices.GetRequiredService<IRecurringJobManager>();
            
            jobManager.AddOrUpdate(
                typeof(T).FullName,
                () => app.ApplicationServices.GetRequiredService<T>().Execute(),
                CronExpressions.EverydayAtMidnight
            );
        }
    }
}
