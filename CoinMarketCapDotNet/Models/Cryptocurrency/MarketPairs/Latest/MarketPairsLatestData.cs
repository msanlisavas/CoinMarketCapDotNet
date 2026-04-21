using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.MarketPairs.Latest
{
    public class MarketPairsLatestData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("num_market_pairs")]
        public int? NumMarketPairs { get; set; }
        [JsonPropertyName("market_pairs")]
        public List<MarketPairData> MarketPairs { get; set; }
    }
}
