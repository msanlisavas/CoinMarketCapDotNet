using Newtonsoft.Json;
using System;

namespace CoinMarketCapDotNet.Models.Tools
{
    public class PriceConvertionQuoteData
    {
        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}
