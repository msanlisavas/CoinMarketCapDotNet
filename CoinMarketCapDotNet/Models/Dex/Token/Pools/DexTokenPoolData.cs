using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Token.Pools
{
    public class DexTokenPoolData
    {
        [JsonPropertyName("pool_address")]
        public string? PoolAddress { get; set; }

        [JsonPropertyName("network_slug")]
        public string? NetworkSlug { get; set; }

        [JsonPropertyName("dex_slug")]
        public string? DexSlug { get; set; }

        [JsonPropertyName("base_token_address")]
        public string? BaseTokenAddress { get; set; }

        [JsonPropertyName("quote_token_address")]
        public string? QuoteTokenAddress { get; set; }

        [JsonPropertyName("liquidity_usd")]
        public double? LiquidityUsd { get; set; }

        [JsonPropertyName("volume_24h_usd")]
        public double? Volume24hUsd { get; set; }
    }
}
