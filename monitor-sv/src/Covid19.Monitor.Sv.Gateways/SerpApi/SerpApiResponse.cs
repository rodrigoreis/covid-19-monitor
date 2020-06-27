using System.Collections.Generic;
using Newtonsoft.Json;

namespace Covid19.Monitor.Sv.Gateways.SerpApi
{
    public class SerpApiResponse
    {
        [JsonProperty("shopping_results")]
        public List<ShoppingResult> Content { get; set; }
    }
}