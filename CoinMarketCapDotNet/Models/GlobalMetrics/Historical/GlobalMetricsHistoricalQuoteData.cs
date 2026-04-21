using System.Text.Json.Serialization;
using System;

namespace CoinMarketCapDotNet.Models.GlobalMetrics.Historical
{
    public class GlobalMetricsHistoricalQuoteData
    {
        [JsonPropertyName("total_market_cap")]
        public double? TotalMarketCap { get; set; }

        [JsonPropertyName("total_volume_24h")]
        public double? TotalVolume24h { get; set; }

        [JsonPropertyName("total_volume_24h_reported")]
        public double? TotalVolume24hReported { get; set; }

        [JsonPropertyName("altcoin_market_cap")]
        public double? AltcoinMarketCap { get; set; }

        [JsonPropertyName("altcoin_volume_24h")]
        public double? AltcoinVolume24h { get; set; }

        [JsonPropertyName("altcoin_volume_24h_reported")]
        public double? AltcoinVolume24hReported { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }
    }
}
