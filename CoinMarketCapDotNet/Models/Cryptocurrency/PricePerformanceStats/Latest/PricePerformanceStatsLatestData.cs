using CoinMarketCapDotNet.Models.General;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.PricePerformanceStats.Latest
{
    public class PricePerformanceStatsLatestData
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("last_updated")]
        public string LastUpdated { get; set; }

        [JsonProperty("quote")]
        public Dictionary<string, TimePeriodQuoteData> Quotes { get; set; }
    }
}
