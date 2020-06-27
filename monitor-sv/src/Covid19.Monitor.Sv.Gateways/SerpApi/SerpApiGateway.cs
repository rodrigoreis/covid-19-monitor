using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Covid19.Monitor.Sv.Gateways.SerpApi
{
    internal class SerpApiGateway : ISerpApiGateway
    {
        private readonly string _apiKey;

        public SerpApiGateway(IConfiguration configuration)
        {
            _apiKey = configuration.GetValue<string>("SerpApiApiKey");
        }

        public async Task<List<ShoppingResult>> ListProductsAsync(string keywords)
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://serpapi.com/")
            };

            const string engine = "engine=google&google_domain=google.com.br";
            const string filters = "location=brazil&gl=br&hl=pt&tbm=shop&no_cache=true";
            
            var improvedKeywords = keywords.Replace(" ", "+");
            var requestUri = $"search.json?q={improvedKeywords}&{engine}&{filters}&api_key={_apiKey}";
            var json = await httpClient.GetStringAsync(requestUri);
            var response = JsonConvert.DeserializeObject<SerpApiResponse>(json);

            return response.Content;
        }
    }
}