using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Quotes
{
    public class HistoricalQuoteData
    {
        [JsonPropertyName("timestamp")]
        public string? Timestamp { get; set; }

        [JsonPropertyName("search_interval")]
        public string? SearchInterval { get; set; }

        [JsonPropertyName("quote")]
        public Dictionary<string, HistoricalMarketDetailsData>? Quote { get; set; }
    }
}
