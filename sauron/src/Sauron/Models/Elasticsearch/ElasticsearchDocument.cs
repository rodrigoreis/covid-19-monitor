using Newtonsoft.Json;

namespace Sauron.Models.Elasticsearch
{
    public class ElasticsearchDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}