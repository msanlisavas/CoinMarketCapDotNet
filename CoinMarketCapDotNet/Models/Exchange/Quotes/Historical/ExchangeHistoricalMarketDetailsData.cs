using System.Text.Json.Serialization;
using System;

namespace CoinMarketCapDotNet.Models.Exchange.Quotes.Historical
{
    public class ExchangeHistoricalMarketDetailsData
    {
        [JsonPropertyName("volume_24hr")]
        public double? Volume24hr { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }
    }
}
