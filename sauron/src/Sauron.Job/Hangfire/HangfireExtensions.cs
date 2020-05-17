using System;
using System.Collections.Generic;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sauron.Application.Abstractions.UseCases;

namespace Sauron.Job.Hangfire
{
    public static class HangfireExtensions
    {
        private static readonly IDictionary<Type, object> RecurringJobs = new Dictionary<Type, object>();

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

        public static void AddRecurringJob<THandler, TInput>(this IServiceCollection services, TInput input)
            where THandler : IUseCase<TInput>
        {
            RecurringJobs.Add(typeof(THandler), input);
        }

        public static void UsingHangFireHandler(this IApplicationBuilder app, string pathMatch = "/dashboard")
        {
            app.UseHangfireServer();

            app.UseHangfireDashboard(pathMatch, new DashboardOptions
            {
                Authorization = new[] { new AllowAuthorizationFilter() }
            });
        }

        public static void UseRecurringJobs(this IApplicationBuilder app)
        {
            var jobManager = app.ApplicationServices.GetRequiredService<IRecurringJobManager>();

            foreach (var (useCase, useCaseInput) in RecurringJobs)
            {
                var service = app.ApplicationServices.GetRequiredService(useCase);
                var method = service.GetType().GetMethod("Execute");
                var recurringJob = new global::Hangfire.Common.Job(method, useCaseInput);

                jobManager.AddOrUpdate(service.GetType().FullName, recurringJob, CronExpressions.EverydayAtMidnight);
            }
        }
    }
}
