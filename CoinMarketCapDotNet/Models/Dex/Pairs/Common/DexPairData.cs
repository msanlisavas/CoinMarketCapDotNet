using System;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Pairs.Common
{
    public class DexPairData
    {
        [JsonPropertyName("pair_address")]
        public string? PairAddress { get; set; }

        [JsonPropertyName("network_slug")]
        public string? NetworkSlug { get; set; }

        [JsonPropertyName("dex_slug")]
        public string? DexSlug { get; set; }

        [JsonPropertyName("base_token_address")]
        public string? BaseTokenAddress { get; set; }

        [JsonPropertyName("base_token_symbol")]
        public string? BaseTokenSymbol { get; set; }

        [JsonPropertyName("quote_token_address")]
        public string? QuoteTokenAddress { get; set; }

        [JsonPropertyName("quote_token_symbol")]
        public string? QuoteTokenSymbol { get; set; }

        [JsonPropertyName("price_usd")]
        public double? PriceUsd { get; set; }

        [JsonPropertyName("liquidity_usd")]
        public double? LiquidityUsd { get; set; }

        [JsonPropertyName("volume_24h_usd")]
        public double? Volume24hUsd { get; set; }

        [JsonPropertyName("price_change_24h_percent")]
        public double? PriceChange24hPercent { get; set; }

        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
    }
}
