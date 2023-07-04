using CoinMarketCapDotNet.Models.Cryptocurrency.MarketPairs;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Exchange.MarketPairs.Latest
{
    public class ExchangeMarketPairLatestData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("num_market_pairs")]
        public int? NumMarketPairs { get; set; }

        [JsonProperty("volume_24h")]
        public double? Volume24h { get; set; }
        [JsonProperty("market_pairs")]
        public List<MarketPairData> MarketPairs { get; set; }

    }
}
