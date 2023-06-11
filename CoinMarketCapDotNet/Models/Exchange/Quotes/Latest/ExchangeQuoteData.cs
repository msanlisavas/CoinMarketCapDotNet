using Newtonsoft.Json;
using System;

namespace CoinMarketCapDotNet.Models.Exchange.Quotes.Latest
{
    public class ExchangeQuoteData
    {
        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("volume_24h")]
        public double Volume24h { get; set; }

        [JsonProperty("volume_24h_adjusted")]
        public double Volume24hAdjusted { get; set; }

        [JsonProperty("volume_7d")]
        public double Volume7d { get; set; }

        [JsonProperty("volume_30d")]
        public double Volume30d { get; set; }

        [JsonProperty("percent_change_volume_24h")]
        public double PercentChangeVolume24h { get; set; }

        [JsonProperty("percent_change_volume_7d")]
        public double PercentChangeVolume7d { get; set; }

        [JsonProperty("percent_change_volume_30d")]
        public double PercentChangeVolume30d { get; set; }

        [JsonProperty("effective_liquidity_24h")]
        public double EffectiveLiquidity24h { get; set; }

        [JsonProperty("derivative_volume")]
        public double DerivativeVolume { get; set; }

        [JsonProperty("spot_volume")]
        public double SpotVolume { get; set; }
    }
}
