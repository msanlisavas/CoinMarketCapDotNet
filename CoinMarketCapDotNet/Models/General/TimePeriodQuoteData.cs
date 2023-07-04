using Newtonsoft.Json;
using System;

namespace CoinMarketCapDotNet.Models.General
{
    public class TimePeriodQuoteData
    {
        [JsonProperty("open")]
        public decimal? Open { get; set; }

        [JsonProperty("open_timestamp")]
        public DateTime OpenTimestamp { get; set; }

        [JsonProperty("high")]
        public decimal? High { get; set; }

        [JsonProperty("high_timestamp")]
        public DateTime HighTimestamp { get; set; }

        [JsonProperty("low")]
        public decimal? Low { get; set; }

        [JsonProperty("low_timestamp")]
        public DateTime LowTimestamp { get; set; }

        [JsonProperty("close")]
        public decimal? Close { get; set; }

        [JsonProperty("close_timestamp")]
        public DateTime CloseTimestamp { get; set; }

        [JsonProperty("percent_change")]
        public decimal? PercentChange { get; set; }

        [JsonProperty("price_change")]
        public decimal? PriceChange { get; set; }
    }
}
