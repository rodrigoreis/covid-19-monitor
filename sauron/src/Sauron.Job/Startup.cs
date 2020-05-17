using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sauron.Domain.Jobs;
using Sauron.Infrastructure.Hangfire;
using Sauron.Infrastructure.Jobs;

namespace Sauron.Job
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDownloadContent, DownloadContent>();
            
            services.AddHangFireHandler();
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
            app.UseRunJobEverydayAtMidnight<IDownloadContent>();
        }
    }
}
