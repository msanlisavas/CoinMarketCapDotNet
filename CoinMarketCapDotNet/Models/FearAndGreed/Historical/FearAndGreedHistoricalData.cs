using System;
using System.Text.Json.Serialization;
using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.FearAndGreed.Historical
{
    public class FearAndGreedHistoricalData
    {
        [JsonPropertyName("value")]
        public int? Value { get; set; }

        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixTimestampDateTimeConverter))]
        public DateTime? Timestamp { get; set; }

        [JsonPropertyName("value_classification")]
        public string? ValueClassification { get; set; }
    }
}
