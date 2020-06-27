using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Covid19.Monitor.Sv.Gateways.IpData
{
    internal class IpDataGateway : IIpDataGateway
    {
        private readonly string _apiKey;

        public IpDataGateway(IConfiguration configuration)
        {
            _apiKey = configuration.GetValue<string>("IpDataApiKey");
        }
        
        public async Task<IpDataInfo> GetIpDataInfoAsync(string ip)
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.ipdata.co/")
            };
            
            const string fields = "ip,city,region_code,latitude,longitude";
            
            var json = await httpClient.GetStringAsync($"{ip}?api-key={_apiKey}&fields={fields}");
            
            return JsonConvert.DeserializeObject<IpDataInfo>(json);
        }
    }
}