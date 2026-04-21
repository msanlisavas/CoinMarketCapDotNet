using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Exchange.Quotes.Historical
{
    public class ExchangeHistoricalQuotesData
    {
        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }

        [JsonPropertyName("num_market_pairs")]
        public double? NumMarketPairs { get; set; }
        [JsonPropertyName("quote")]
        public Dictionary<string, ExchangeHistoricalMarketDetailsData> Quote { get; set; }
    }
}
