using System;
using Newtonsoft.Json;

namespace Covid19.Monitor.Sv.Gateways.BrasilIo
{
    public class CasesInfo
    {
        [JsonProperty("state")]
        public string RegionCode { get; set; }
        
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        
        [JsonProperty("last_available_confirmed")]
        public int? CurrentConfirmed { get; set; }
        
        [JsonProperty("new_confirmed")]
        public int? NewConfirmed { get; set; }
        
        [JsonProperty("last_available_deaths")]
        public int? CurrentConfirmedDeaths { get; set; }
        
        [JsonProperty("new_deaths")]
        public int? NewConfirmedDeaths { get; set; }
        
        [JsonProperty("last_available_death_rate")]
        public double? DeathRate { get; set; }
    }
}