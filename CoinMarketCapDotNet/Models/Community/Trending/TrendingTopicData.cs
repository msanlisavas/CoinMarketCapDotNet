using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Community.Trending
{
    public class TrendingTopicData
    {
        [JsonProperty("rank")]
        public double? Rank { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }
    }
}
