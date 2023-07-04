using CoinMarketCapDotNet.Models.General;
using Newtonsoft.Json;
using System;
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
        public DateTime? LastUpdated { get; set; }
        [JsonProperty("periods")]
        public Dictionary<string, TimePeriodData> Periods { get; set; }

    }
}
