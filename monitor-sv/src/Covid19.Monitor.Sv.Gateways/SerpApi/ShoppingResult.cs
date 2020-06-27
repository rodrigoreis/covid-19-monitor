using Newtonsoft.Json;

namespace Covid19.Monitor.Sv.Gateways.SerpApi
{
    public class ShoppingResult
    {
        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }
        
        [JsonProperty("extracted_price")]
        public decimal ExtractedPrice { get; set; }

        [JsonProperty("snippet")]
        public string Snippet { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
    }
}


