using System.Text.Json.Serialization;
using System;
using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Exchange.Map
{
    public class ExchangeMapData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        [JsonPropertyName("is_active")]
        [JsonConverter(typeof(NumericBoolConverter))]
        public bool IsActive { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("first_historical_data")]
        public DateTime? FirstHistoricalData { get; set; }

        [JsonPropertyName("last_historical_data")]
        public DateTime? LastHistoricalData { get; set; }
    }
}
