using System;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Kline.Points
{
    public class DexKlinePointData
    {
        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }

        [JsonPropertyName("price_usd")]
        public double? PriceUsd { get; set; }

        [JsonPropertyName("volume_usd")]
        public double? VolumeUsd { get; set; }
    }
}
