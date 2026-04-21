using System.Text.Json.Serialization;
using System;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Quotes
{
    public class HistoricalMarketDetailsData
    {
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }

        [JsonPropertyName("volume_24hr")]
        public decimal? Volume24hr { get; set; }

        [JsonPropertyName("market_cap")]
        public decimal? MarketCap { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }
    }
}
