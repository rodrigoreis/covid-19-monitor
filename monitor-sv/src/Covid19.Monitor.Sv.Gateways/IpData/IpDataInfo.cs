using Newtonsoft.Json;

namespace Covid19.Monitor.Sv.Gateways.IpData
{
    public class IpDataInfo
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }
        
        [JsonProperty("city")]
        public string City { get; set; }
        
        [JsonProperty("region_code")]
        public string Region { get; set; }
        
        [JsonProperty("latitude")]
        public string Latitude { get; set; }
        
        [JsonProperty("longitude")]
        public string Longitude { get; set; }
    }
}