using System;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Token.LiquidityChange
{
    public class DexTokenLiquidityChangeData
    {
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("network_slug")]
        public string? NetworkSlug { get; set; }

        [JsonPropertyName("liquidity_usd")]
        public double? LiquidityUsd { get; set; }

        [JsonPropertyName("change_percent")]
        public double? ChangePercent { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }
    }
}
