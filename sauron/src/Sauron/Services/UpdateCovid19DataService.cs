using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sauron.Models.Covid19DataApi;

namespace Sauron.Services
{
    public class UpdateCovid19DataService : IUpdateCovid19DataService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UpdateCovid19DataService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private async Task BulkPostToElasticsearch(string catalog, string payload)
        {
            using var client = _httpClientFactory.CreateClient("ElasticsearchApi");
            var response = await client.PostAsync($"{catalog}/_bulk"
                , new StringContent(string.Concat(payload, $"{Environment.NewLine}"), Encoding.UTF8
                    , "application/json"));
            if (!response.IsSuccessStatusCode)
                throw new OperationCanceledException($"failure when performs post \"{catalog}/_bulk\"");
        }

        public async Task UpdateFullDataAsync()
        {
            using var client = _httpClientFactory.CreateClient("Covid19DataApi");
            var response = await client.GetAsync("caso_full/data");
            if (!response.IsSuccessStatusCode)
                throw new OperationCanceledException("failure when performs get \"caso_full/data\"");
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert
                .DeserializeObject<Covid19DataApiResponse<Covid19DataApiFullDataItem>>(responseString);
            var payload = string.Join($"{Environment.NewLine}"
                , data.Results.Select(doc =>
                    $"{{\"index\": {{}}}}{Environment.NewLine}{JsonConvert.SerializeObject(doc)}").ToList());
            await BulkPostToElasticsearch("brasil_io_dataset_covid19_full_data", payload);
        }

        public Task UpdateBulletinDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateNotaryDeathsAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateFullData()
        {
            UpdateFullDataAsync().Wait();
        }

        public void UpdateBulletinData()
        {
            UpdateBulletinDataAsync().Wait();
        }

        public void UpdateNotaryDeaths()
        {
            UpdateNotaryDeathsAsync().Wait();
        }
    }
}