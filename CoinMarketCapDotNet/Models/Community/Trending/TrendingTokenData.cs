using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Community.Trending
{
    public class TrendingTokenData
    {
        [JsonProperty("community_rank")]
        public int CommunityRank { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("cmc_rank")]
        public int CmcRank { get; set; }
    }
}
