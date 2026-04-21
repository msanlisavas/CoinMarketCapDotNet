using System;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Kline.Candles
{
    public class DexKlineCandleData
    {
        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }

        [JsonPropertyName("open")]
        public double? Open { get; set; }

        [JsonPropertyName("high")]
        public double? High { get; set; }

        [JsonPropertyName("low")]
        public double? Low { get; set; }

        [JsonPropertyName("close")]
        public double? Close { get; set; }

        [JsonPropertyName("volume_usd")]
        public double? VolumeUsd { get; set; }

        [JsonPropertyName("trader_count")]
        public int? TraderCount { get; set; }
    }
}
