using System.Text.Json.Serialization;
using System;

namespace CoinMarketCapDotNet.Models.Exchange.Quotes.Latest
{
    public class ExchangeQuoteData
    {
        [JsonPropertyName("last_updated")]
        public DateTime? LastUpdated { get; set; }

        [JsonPropertyName("volume_24h")]
        public double? Volume24h { get; set; }

        [JsonPropertyName("volume_24h_adjusted")]
        public double? Volume24hAdjusted { get; set; }

        [JsonPropertyName("volume_7d")]
        public double? Volume7d { get; set; }

        [JsonPropertyName("volume_30d")]
        public double? Volume30d { get; set; }

        [JsonPropertyName("percent_change_volume_24h")]
        public double? PercentChangeVolume24h { get; set; }

        [JsonPropertyName("percent_change_volume_7d")]
        public double? PercentChangeVolume7d { get; set; }

        [JsonPropertyName("percent_change_volume_30d")]
        public double? PercentChangeVolume30d { get; set; }

        [JsonPropertyName("effective_liquidity_24h")]
        public double? EffectiveLiquidity24h { get; set; }

        [JsonPropertyName("derivative_volume")]
        public double? DerivativeVolume { get; set; }

        [JsonPropertyName("spot_volume")]
        public double? SpotVolume { get; set; }
    }
}
