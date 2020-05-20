using System.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sauron.Application;
using Sauron.Application.UseCases.Downloads;
using Sauron.Job.Hangfire;

namespace Sauron.Job
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddUseCases();
            services.AddHangFireHandler();
            services.AddRecurringJob<IDownloadHtmlPageUseCase, DownloadHtmlPageInput>(
                new DownloadHtmlPageInput("http://www.uol.com.br"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Sauron"); });
            });

             app.UsingHangFireHandler();
             app.UseRecurringJobs();
             
            // app.UseRunJobEverydayAtMidnight<IDownloadContent>();
        }
    }
}
