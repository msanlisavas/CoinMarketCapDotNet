using System.Text.Json.Serialization;
using System;

namespace CoinMarketCapDotNet.Models.General
{
    public class QuoteData
    {
        // V3-only identification fields (null on V1/V2 responses, where the currency is the dict key).
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("price")]
        public double? Price { get; set; }

        [JsonPropertyName("volume_24h")]
        public double? Volume24h { get; set; }

        [JsonPropertyName("volume_change_24h")]
        public double? VolumeChange24h { get; set; }

        // V3-only volume split (null on V1/V2).
        [JsonPropertyName("cex_volume_24h")]
        public double? CexVolume24h { get; set; }

        [JsonPropertyName("dex_volume_24h")]
        public double? DexVolume24h { get; set; }

        [JsonPropertyName("percent_change_1h")]
        public double? PercentChange1h { get; set; }

        [JsonPropertyName("percent_change_24h")]
        public double? PercentChange24h { get; set; }

        [JsonPropertyName("percent_change_7d")]
        public double? PercentChange7d { get; set; }

        [JsonPropertyName("percent_change_30d")]
        public double? PercentChange30d { get; set; }

        [JsonPropertyName("percent_change_60d")]
        public double? PercentChange60d { get; set; }

        [JsonPropertyName("percent_change_90d")]
        public double? PercentChange90d { get; set; }

        [JsonPropertyName("market_cap")]
        public double? MarketCap { get; set; }

        [JsonPropertyName("market_cap_dominance")]
        public double? MarketCapDominance { get; set; }

        [JsonPropertyName("fully_diluted_market_cap")]
        public double? FullyDilutedMarketCap { get; set; }

        // V3-only (null on V1/V2).
        [JsonPropertyName("minted_market_cap")]
        public double? MintedMarketCap { get; set; }

        [JsonPropertyName("tvl")]
        public object? Tvl { get; set; } // This can be changed to appropriate type

        [JsonPropertyName("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}
