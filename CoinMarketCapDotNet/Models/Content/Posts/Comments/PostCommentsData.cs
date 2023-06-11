using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Content.Posts.Comments
{
    public class PostCommentsData
    {
        [JsonProperty("post_id")]
        public string PostId { get; set; }

        [JsonProperty("owner")]
        public OwnerData Owner { get; set; }

        [JsonProperty("text_content")]
        public string TextContent { get; set; }

        [JsonProperty("photos")]
        public List<string> Photos { get; set; }

        [JsonProperty("comment_count")]
        public string CommentCount { get; set; }

        [JsonProperty("like_count")]
        public string LikeCount { get; set; }

        [JsonProperty("post_time")]
        public string PostTime { get; set; }

        [JsonProperty("language_code")]
        public string LanguageCode { get; set; }

        [JsonProperty("comments_url")]
        public string CommentsUrl { get; set; }
    }
}
