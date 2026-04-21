using System;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Holders.Trend
{
    public class DexHoldersTrendData
    {
        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }

        [JsonPropertyName("holder_count")]
        public int? HolderCount { get; set; }

        [JsonPropertyName("top_10_holding_ratio")]
        public double? Top10HoldingRatio { get; set; }

        [JsonPropertyName("top_50_holding_ratio")]
        public double? Top50HoldingRatio { get; set; }

        [JsonPropertyName("top_100_holding_ratio")]
        public double? Top100HoldingRatio { get; set; }
    }
}
