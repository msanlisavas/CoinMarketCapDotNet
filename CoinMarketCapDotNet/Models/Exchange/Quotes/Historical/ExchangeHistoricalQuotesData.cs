using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Exchange.Quotes.Historical
{
    public class ExchangeHistoricalQuotesData
    {
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("num_market_pairs")]
        public double NumMarketPairs { get; set; }
        [JsonProperty("quote")]
        public Dictionary<string, ExchangeHistoricalMarketDetailsData> Quote { get; set; }
    }
}
