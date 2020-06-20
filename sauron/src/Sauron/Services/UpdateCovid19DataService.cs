using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
            await _elasticsearchService.CreateIndexIfNotExists(index,
                @"{
					""settings"": {
						""index"": {
							""number_of_shards"": 3,
							""number_of_replicas"": 0
						}
					},
					""mappings"": {
						""properties"": {
							""city"": {
								""type"": ""text"",
								""fields"": {
									""original"": {
										""type"": ""keyword"",
										""index"": true
									}
								},
								""index"": true,
								""analyzer"": ""portuguese""
							},
							""city_ibge_code"": {
								""type"": ""integer"",
								""index"": true
							},
							""date"": {
								""type"": ""date"",
								""index"": true
							},
							""epidemiological_week"": {
								""type"": ""integer"",
								""index"": true
							},
							""estimated_population_2019"": {
								""type"": ""integer"",
								""index"": true
							},
							""is_last"": {
								""type"": ""boolean"",
								""index"": true
							},
							""is_repeated"": {
								""type"": ""boolean"",
								""index"": true
							},
							""last_available_confirmed"": {
								""type"": ""integer"",
								""index"": true
							},
							""last_available_confirmed_per_100k_inhabitants"": {
								""type"": ""float"",
								""index"": true
							},
							""last_available_date"": {
								""type"": ""date"", 
								""index"": true
							},
							""last_available_death_rate"": {
								""type"": ""float"", 
								""index"": true
							},
							""last_available_deaths"": {
								""type"": ""integer"", 
								""index"": true
							},
							""new_confirmed"": {
								""type"": ""integer"", 
								""index"": true
							},
							""new_deaths"": {
								""type"": ""integer"", 
								""index"": true
							},
							""order_for_place"": {
								""type"": ""integer"", 
								""index"": true
							},
							""place_type"": {
								""type"": ""text"",
								""fields"": {
									""original"": {
										""type"": ""keyword"",
										""index"": true
									}
								},
								""index"": true,
								""analyzer"": ""english""
							},
							""state"": {
								""type"": ""text"",
								""fields"": {
									""original"": {
										""type"": ""keyword"",
										""index"": true
									}
								},
								""index"": true,
								""analyzer"": ""portuguese""
							}
						}
					}
				}");

            while (true)
            {
                using var client = _httpClientFactory.CreateClient("Covid19DataApi");
                var response = await client.GetAsync(url);
                await response.CheckIsSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<DataApiResponse<FullDataItem>>(responseString);
                data?.Results
                    .Where(item => item.City != default && item.Date.HasValue)
                    .ToList()
                    .ForEach(item =>
                    {
                        var temp = $"{Regex.Replace(item.City, @"\s+", "-")}-{item.State}-{item.Date?.ToString("s")}"
                            .ToLower();
                        item.Id = Convert.ToBase64String(Encoding.UTF8.GetBytes(temp));
                    });
                await _elasticsearchService.BulkInsert(index, data?.Results);

                if (data?.Next != default)
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