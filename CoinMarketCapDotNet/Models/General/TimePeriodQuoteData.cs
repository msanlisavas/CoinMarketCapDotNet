using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.General
{
    public class TimePeriodQuoteData
    {
        [JsonProperty("open")]
        public decimal Open { get; set; }

        [JsonProperty("open_timestamp")]
        public string OpenTimestamp { get; set; }

        [JsonProperty("high")]
        public decimal High { get; set; }

        [JsonProperty("high_timestamp")]
        public string HighTimestamp { get; set; }

        [JsonProperty("low")]
        public decimal Low { get; set; }

        [JsonProperty("low_timestamp")]
        public string LowTimestamp { get; set; }

        [JsonProperty("close")]
        public decimal Close { get; set; }

        [JsonProperty("close_timestamp")]
        public string CloseTimestamp { get; set; }

        [JsonProperty("percent_change")]
        public decimal PercentChange { get; set; }

        [JsonProperty("price_change")]
        public decimal PriceChange { get; set; }
    }
}
