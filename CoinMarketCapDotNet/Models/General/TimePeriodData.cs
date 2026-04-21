using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.General
{
    public class TimePeriodData
    {
        [JsonPropertyName("open_timestamp")]
        public string? OpenTimestamp { get; set; }

        [JsonPropertyName("high_timestamp")]
        public string? HighTimestamp { get; set; }

        [JsonPropertyName("low_timestamp")]
        public string? LowTimestamp { get; set; }

        [JsonPropertyName("close_timestamp")]
        public string? CloseTimestamp { get; set; }

        [JsonPropertyName("quote")]
        public Dictionary<string, TimePeriodQuoteData>? Quote { get; set; }
    }
}
