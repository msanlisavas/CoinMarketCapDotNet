using CoinMarketCapDotNet.Models.General;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.PricePerformanceStats.Latest
{
    public class PricePerformanceStatsLatestData
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime? LastUpdated { get; set; }
        [JsonPropertyName("periods")]
        public Dictionary<string, TimePeriodData> Periods { get; set; }

    }
}
