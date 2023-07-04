using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.MarketPairs.Latest
{
    public class MarketPairsLatestData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("num_market_pairs")]
        public int? NumMarketPairs { get; set; }
        [JsonProperty("market_pairs")]
        public List<MarketPairData> MarketPairs { get; set; }
    }
}
