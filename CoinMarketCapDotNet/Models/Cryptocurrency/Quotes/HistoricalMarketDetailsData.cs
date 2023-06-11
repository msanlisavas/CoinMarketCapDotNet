using Newtonsoft.Json;
using System;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Quotes
{
    public class HistoricalMarketDetailsData
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("volume_24hr")]
        public decimal Volume24hr { get; set; }

        [JsonProperty("market_cap")]
        public decimal MarketCap { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
