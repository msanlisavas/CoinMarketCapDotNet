using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Community.Trending
{
    public class TrendingTopicData
    {
        [JsonPropertyName("rank")]
        public double? Rank { get; set; }
        [JsonPropertyName("topic")]
        public string? Topic { get; set; }
    }
}
