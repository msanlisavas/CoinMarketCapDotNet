using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.General
{
    public class CoinsData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("cmc_rank")]
        public int? CmcRank { get; set; }

        [JsonPropertyName("num_market_pairs")]
        public int? NumMarketPairs { get; set; }

        [JsonPropertyName("circulating_supply")]
        public double? CirculatingSupply { get; set; }

        [JsonPropertyName("total_supply")]
        public double? TotalSupply { get; set; }

        [JsonPropertyName("market_cap_by_total_supply")]
        public double? MarketCapByTotalSupply { get; set; }

        [JsonPropertyName("max_supply")]
        public double? MaxSupply { get; set; }

        [JsonPropertyName("last_updated")]
        public string LastUpdated { get; set; }

        [JsonPropertyName("date_added")]
        public string DateAdded { get; set; }
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }
        [JsonPropertyName("platform")]
        public PlatformData Platform { get; set; }
        [JsonPropertyName("quote")]
        public Dictionary<string, QuoteData> Quotes { get; set; }
    }
}
