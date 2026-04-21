using System;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.FearAndGreed.Historical
{
    public class FearAndGreedHistoricalData
    {
        [JsonPropertyName("value")]
        public int? Value { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }

        [JsonPropertyName("value_classification")]
        public string? ValueClassification { get; set; }
    }
}
