using Newtonsoft.Json;
using System;

namespace CoinMarketCapDotNet.Models.Exchange.Map
{
    public class ExchangeMapData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("first_historical_data")]
        public DateTime FirstHistoricalData { get; set; }

        [JsonProperty("last_historical_data")]
        public DateTime LastHistoricalData { get; set; }
    }
}
