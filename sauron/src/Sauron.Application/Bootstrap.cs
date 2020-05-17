using Microsoft.Extensions.DependencyInjection;
using Sauron.Application.UseCases.Downloads;

namespace Sauron.Application
{
    public static class Bootstrap
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddSingleton<IDownloadHtmlPageUseCase, DownloadHtmlPageUseCase>();
            
            return services;
        }
    }
}
