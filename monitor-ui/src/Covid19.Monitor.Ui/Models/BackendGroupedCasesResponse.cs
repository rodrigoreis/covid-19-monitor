using System.Text.Json.Serialization;

namespace Covid19.Monitor.Ui.Models
{
    public class BackendGroupedCasesResponse
    {
        [JsonPropertyName("month")]
        public int Month { get; set; }
        
        [JsonPropertyName("currentCases")]
        public int? CurrentCases { get; set; }
        
        [JsonPropertyName("newCases")]
        public int? NewCases { get; set; }
        
        [JsonPropertyName("currentDeaths")]
        public int? CurrentDeaths { get; set; }
        
        [JsonPropertyName("newDeaths")]
        public int? NewDeaths { get; set; }
    }
}