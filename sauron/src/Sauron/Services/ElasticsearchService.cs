using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Sauron.Extensions.Http;

namespace Sauron.Services
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ElasticsearchService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task BulkInsert<T>(string index, IEnumerable<T> documents)
        {
            using var client = _httpClientFactory.CreateClient("ElasticsearchApi");
            var payload = documents.CreateBulkInsertPayload();
            var response = await client.PostAsync($"{index}/_bulk",
                new StringContent(payload, Encoding.UTF8, "application/json"));
            await response.CheckIsSuccessStatusCode();
        }
    }
}