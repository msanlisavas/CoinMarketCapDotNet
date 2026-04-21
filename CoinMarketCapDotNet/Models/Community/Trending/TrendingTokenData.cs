using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Community.Trending
{
    public class TrendingTokenData
    {
        [JsonPropertyName("community_rank")]
        public int? CommunityRank { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        [JsonPropertyName("cmc_rank")]
        public int? CmcRank { get; set; }
    }
}
