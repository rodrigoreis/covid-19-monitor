using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sauron.Models.Covid19DataApi
{
    public class DataApiResponse<T>
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        
        [JsonProperty("next")]
        public string Next { get; set; }
        
        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("results")]
        public List<T> Results { get; set; }
    }
}