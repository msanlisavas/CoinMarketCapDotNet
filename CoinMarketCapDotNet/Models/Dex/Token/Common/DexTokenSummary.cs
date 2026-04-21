using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Token.Common
{
    public class DexTokenSummary
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("network_slug")]
        public string? NetworkSlug { get; set; }

        [JsonPropertyName("price_usd")]
        public double? PriceUsd { get; set; }

        [JsonPropertyName("volume_24h_usd")]
        public double? Volume24hUsd { get; set; }

        [JsonPropertyName("market_cap_usd")]
        public double? MarketCapUsd { get; set; }

        [JsonPropertyName("liquidity_usd")]
        public double? LiquidityUsd { get; set; }

        [JsonPropertyName("price_change_24h_percent")]
        public double? PriceChange24hPercent { get; set; }
    }
}
