using CoinMarketCapDotNet.Models.General;
using System.Text.Json.Serialization;
using System;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Map
{
    public class MapData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("rank")]
        public int? Rank { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("first_historical_data")]
        public DateTime? FirstHistoricalData { get; set; }

        [JsonPropertyName("last_historical_data")]
        public DateTime? LastHistoricalData { get; set; }

        [JsonPropertyName("platform")]
        public PlatformData Platform { get; set; }
    }
}
