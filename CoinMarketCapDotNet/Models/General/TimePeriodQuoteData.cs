using System.Text.Json.Serialization;
using System;

namespace CoinMarketCapDotNet.Models.General
{
    public class TimePeriodQuoteData
    {
        [JsonPropertyName("open")]
        public decimal? Open { get; set; }

        [JsonPropertyName("open_timestamp")]
        public DateTime OpenTimestamp { get; set; }

        [JsonPropertyName("high")]
        public decimal? High { get; set; }

        [JsonPropertyName("high_timestamp")]
        public DateTime HighTimestamp { get; set; }

        [JsonPropertyName("low")]
        public decimal? Low { get; set; }

        [JsonPropertyName("low_timestamp")]
        public DateTime LowTimestamp { get; set; }

        [JsonPropertyName("close")]
        public decimal? Close { get; set; }

        [JsonPropertyName("close_timestamp")]
        public DateTime CloseTimestamp { get; set; }

        [JsonPropertyName("percent_change")]
        public decimal? PercentChange { get; set; }

        [JsonPropertyName("price_change")]
        public decimal? PriceChange { get; set; }
    }
}
