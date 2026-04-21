using System;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.FearAndGreed.Latest
{
    public class FearAndGreedLatestData
    {
        [JsonPropertyName("value")]
        public int? Value { get; set; }

        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }

        [JsonPropertyName("value_classification")]
        public string? ValueClassification { get; set; }
    }
}
