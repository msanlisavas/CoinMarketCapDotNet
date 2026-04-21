using System;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Token.Liquidity
{
    public class DexTokenLiquidityData
    {
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("network_slug")]
        public string? NetworkSlug { get; set; }

        [JsonPropertyName("liquidity_usd")]
        public double? LiquidityUsd { get; set; }

        [JsonPropertyName("pool_count")]
        public int? PoolCount { get; set; }

        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
    }
}
