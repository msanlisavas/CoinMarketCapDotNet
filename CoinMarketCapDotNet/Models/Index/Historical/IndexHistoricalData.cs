using System;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Index.Historical
{
    public class IndexHistoricalData
    {
        [JsonPropertyName("value")]
        public double? Value { get; set; }

        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }
    }
}
