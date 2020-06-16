using System;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sauron.Services;

namespace Sauron.Extensions.Hangfire
{
    public static class HangfireExtensions
    {
        public static void AddHangFireHandler(this IServiceCollection services)
        {
            services.AddSingleton<IUpdateCovid19DataService, UpdateCovid19DataService>();

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
                Authorization = new[] { new AllowAnonymousAuthorizationFilter() }
            });
        }

        public static void UseRecurringJobs(this IApplicationBuilder app)
        {
            var recurringJobManager = app.ApplicationServices
                .GetRequiredService<IRecurringJobManager>();

            var updateCovid19DataService = app.ApplicationServices
                .GetRequiredService<IUpdateCovid19DataService>();

            recurringJobManager
                .AddOrUpdate(
                    nameof(updateCovid19DataService.UpdateFullData),
                    () => updateCovid19DataService.UpdateFullData(),
                    CronExpressions.EverydayAtMidnight
                );
        }
    }
}