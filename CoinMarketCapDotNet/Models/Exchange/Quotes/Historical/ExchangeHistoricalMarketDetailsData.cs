using Newtonsoft.Json;
using System;

namespace CoinMarketCapDotNet.Models.Exchange.Quotes.Historical
{
    public class ExchangeHistoricalMarketDetailsData
    {
        [JsonProperty("volume_24hr")]
        public double Volume24hr { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
