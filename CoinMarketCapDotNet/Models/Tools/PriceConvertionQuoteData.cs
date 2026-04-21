using System.Text.Json.Serialization;
using System;

namespace CoinMarketCapDotNet.Models.Tools
{
    public class PriceConvertionQuoteData
    {
        [JsonPropertyName("price")]
        public double? Price { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime? LastUpdated { get; set; }
    }
}
