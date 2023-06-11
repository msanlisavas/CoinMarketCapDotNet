using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Tools
{
    public class PriceConversionData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }
        [JsonProperty("quote")]
        public Dictionary<string, PriceConvertionQuoteData> Quote { get; set; }
    }
}
