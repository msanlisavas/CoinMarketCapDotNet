using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Content.Posts
{
    public class ContentPostsListData
    {
        [JsonProperty("list")]
        public List<ContentPostsLatestData> List { get; set; }
        [JsonProperty("last_score")]
        public string LastScore { get; set; }
    }
}
