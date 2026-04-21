using CoinMarketCapDotNet.Models.Cryptocurrency.MarketPairs;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Exchange.MarketPairs.Latest
{
    public class ExchangeMarketPairLatestData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        [JsonPropertyName("num_market_pairs")]
        public int? NumMarketPairs { get; set; }

        [JsonPropertyName("volume_24h")]
        public double? Volume24h { get; set; }
        [JsonPropertyName("market_pairs")]
        public List<MarketPairData> MarketPairs { get; set; } = new List<MarketPairData>();

    }
}
