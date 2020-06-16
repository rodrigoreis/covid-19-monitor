using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sauron.Extensions.Http
{
    public static class HttpClientFactoryExtensions
    {
        public static IServiceCollection AddHttpClientFactories(this IServiceCollection services)
        {
            services
                .AddHttpClient("ElasticsearchApi", (provider, client) =>
                {
                    var config = provider.GetRequiredService<IConfiguration>();
                    client.BaseAddress = new Uri(config.GetValue<string>("ElasticsearchApiBaseAddress"));
                });

            services
                .AddHttpClient("Covid19DataApi", (provider, client) =>
                {
                    var config = provider.GetRequiredService<IConfiguration>();
                    client.BaseAddress = new Uri(config.GetValue<string>("Covid19DataApiBaseAddress"));
                });

            return services;
        }
    }
}