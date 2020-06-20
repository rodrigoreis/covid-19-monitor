using System;
using Newtonsoft.Json;
using Sauron.Models.Elasticsearch;

namespace Sauron.Models.Covid19DataApi
{
    public class FullDataItem : ElasticsearchDocument
    {
        [JsonProperty("city")]
        public string City { get; set; }
        
        [JsonProperty("city_ibge_code")]
        public int? CityIbgeCode { get; set; }
        
        [JsonProperty("date")]
        public DateTime? Date { get; set; }
        
        [JsonProperty("epidemiological_week")]
        public int? EpidemiologicalWeek { get; set; }
        
        [JsonProperty("estimated_population_2019")]
        public int? EstimatedPopulation2019 { get; set; }
        
        [JsonProperty("is_last")]
        public bool? IsLast { get; set; }
        
        [JsonProperty("is_repeated")]
        public bool? IsRepeated { get; set; }
        
        [JsonProperty("last_available_confirmed")]
        public int? LastAvailableConfirmed { get; set; }
        
        [JsonProperty("last_available_confirmed_per_100k_inhabitants")]
        public double? LastAvailableConfirmedPer100KInhabitants { get; set; }
        
        [JsonProperty("last_available_date")]
        public DateTime? LastAvailableDate { get; set; }
        
        [JsonProperty("last_available_death_rate")]
        public double? LastAvailableDeathRate { get; set; }
       
        [JsonProperty("last_available_deaths")]
        public int? LastAvailableDeaths { get; set; }
        
        [JsonProperty("new_confirmed")]
        public int? NewConfirmed { get; set; }
        
        [JsonProperty("new_deaths")]
        public int? NewDeaths { get; set; }
        
        [JsonProperty("order_for_place")]
        public int? OrderForPlace { get; set; }
        
        [JsonProperty("place_type")]
        public string PlaceType { get; set; }
        
        [JsonProperty("state")]
        public string State { get; set; }
    }
}