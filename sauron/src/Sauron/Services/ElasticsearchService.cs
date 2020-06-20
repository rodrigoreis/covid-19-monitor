using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Sauron.Extensions.Http;
using Sauron.Models.Elasticsearch;

namespace Sauron.Services
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ElasticsearchService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task CreateIndexIfNotExists(string index, string contract)
        {
            using var client = _httpClientFactory.CreateClient("ElasticsearchApi");
            var checkResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, index));
            if (checkResponse.StatusCode == HttpStatusCode.NotFound)
            {
                var response = await client.PutAsync($"{index}",
                    new StringContent(contract, Encoding.UTF8, "application/json"));
                await response.CheckIsSuccessStatusCode();
            }
        }

        public async Task BulkInsert<T>(string index, IEnumerable<T> documents) where T : ElasticsearchDocument
        {
            using var client = _httpClientFactory.CreateClient("ElasticsearchApi");
            var payload = documents.CreateBulkInsertPayload();
            var response = await client.PostAsync($"{index}/_bulk",
                new StringContent(payload, Encoding.UTF8, "application/json"));
            await response.CheckIsSuccessStatusCode();
        }
    }
}