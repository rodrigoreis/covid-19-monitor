using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sauron.Extensions.Http;
using Sauron.Models.Covid19DataApi;

namespace Sauron.Services
{
    public class UpdateCovid19DataService : IUpdateCovid19DataService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IElasticsearchService _elasticsearchService;

        public UpdateCovid19DataService(IHttpClientFactory httpClientFactory,
            IElasticsearchService elasticsearchService)
        {
            _httpClientFactory = httpClientFactory;
            _elasticsearchService = elasticsearchService;
        }

        private async Task ProcessRecursiveUpdateDataAsync(string index, string url)
        {
            while (true)
            {
                using var client = _httpClientFactory.CreateClient("Covid19DataApi");
                var response = await client.GetAsync(url);
                await response.CheckIsSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<DataApiResponse<FullDataItem>>(responseString);
                await _elasticsearchService.BulkInsert(index, data.Results);

                if (data.Next != default)
                {
                    url = data.Next;
                    continue;
                }

                break;
            }
        }

        public Task UpdateFullDataAsync()
        {
            return ProcessRecursiveUpdateDataAsync("brasil_io_dataset_covid19_full_data", "caso_full/data");
        }

        public Task UpdateBulletinDataAsync()
        {
            return ProcessRecursiveUpdateDataAsync("brasil_io_dataset_covid19_bulletin", "boletim/data");
        }

        public Task UpdateNotaryDeathsAsync()
        {
            return ProcessRecursiveUpdateDataAsync("brasil_io_dataset_covid19_notary_deaths", "obito_cartorio/data");
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