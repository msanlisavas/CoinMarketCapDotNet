using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Content.Posts
{
    public class ContentPostsListData
    {
        [JsonPropertyName("list")]
        public List<ContentPostsLatestData> List { get; set; }
        [JsonPropertyName("last_score")]
        public string LastScore { get; set; }
    }
}
