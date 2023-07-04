using Newtonsoft.Json;
using System;

namespace CoinMarketCapDotNet.Models.GlobalMetrics.Latest
{
    public class GlobalMetricsLatestQuoteData
    {
        [JsonProperty("total_market_cap")]
        public double? TotalMarketCap { get; set; }

        [JsonProperty("total_volume_24h")]
        public double? TotalVolume24h { get; set; }

        [JsonProperty("total_volume_24h_reported")]
        public double? TotalVolume24hReported { get; set; }

        [JsonProperty("altcoin_volume_24h")]
        public double? AltcoinVolume24h { get; set; }

        [JsonProperty("altcoin_volume_24h_reported")]
        public double? AltcoinVolume24hReported { get; set; }

        [JsonProperty("altcoin_market_cap")]
        public double? AltcoinMarketCap { get; set; }

        [JsonProperty("last_updated")]
        public DateTime? LastUpdated { get; set; }
    }
}
