using System;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Token.Price
{
    public class DexTokenPriceData
    {
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("network_slug")]
        public string? NetworkSlug { get; set; }

        [JsonPropertyName("price_usd")]
        public double? PriceUsd { get; set; }

        [JsonPropertyName("price_native")]
        public double? PriceNative { get; set; }

        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
    }
}
