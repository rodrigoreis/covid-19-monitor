using Covid19.Monitor.Sv.Gateways.IpData;
using Covid19.Monitor.Sv.Gateways.SerpApi;
using Microsoft.Extensions.DependencyInjection;

namespace Covid19.Monitor.Sv.Gateways
{
    public static class Bootstrapper
    {
        public static void AddServiceGateways(this IServiceCollection services)
        {
            services.AddScoped<IIpDataGateway, IpDataGateway>();
            services.AddScoped<ISerpApiGateway, SerpApiGateway>();
        }
    }
}