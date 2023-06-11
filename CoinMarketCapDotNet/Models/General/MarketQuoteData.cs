using Newtonsoft.Json;
using System;

namespace CoinMarketCapDotNet.Models.General
{
    public class MarketQuoteData
    {
        [JsonProperty("open")]
        public decimal Open { get; set; }

        [JsonProperty("high")]
        public decimal High { get; set; }

        [JsonProperty("low")]
        public decimal Low { get; set; }

        [JsonProperty("close")]
        public decimal Close { get; set; }

        [JsonProperty("volume")]
        public decimal Volume { get; set; }

        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }
        [JsonProperty("market_cap")]
        public decimal MarketCap { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }

}
