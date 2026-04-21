using CoinMarketCapDotNet.Models.General;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Ohlcv.Latest
{
    public class OHLCVLatestData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime? LastUpdated { get; set; }

        [JsonPropertyName("time_open")]
        public DateTime? TimeOpen { get; set; }

        [JsonPropertyName("time_high")]
        public DateTime? TimeHigh { get; set; }

        [JsonPropertyName("time_low")]
        public DateTime? TimeLow { get; set; }

        [JsonPropertyName("time_close")]
        public DateTime? TimeClose { get; set; }

        [JsonPropertyName("quote")]
        public Dictionary<string, MarketQuoteData>? Quotes { get; set; }
    }
}
