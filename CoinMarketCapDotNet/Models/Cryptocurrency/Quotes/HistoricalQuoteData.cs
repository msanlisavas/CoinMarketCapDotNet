using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Quotes
{
    public class HistoricalQuoteData
    {
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("search_interval")]
        public string SearchInterval { get; set; }

        [JsonProperty("quote")]
        public Dictionary<string, HistoricalMarketDetailsData> Quote { get; set; }
    }
}
