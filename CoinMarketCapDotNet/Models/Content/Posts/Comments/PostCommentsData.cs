using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Content.Posts.Comments
{
    public class PostCommentsData
    {
        [JsonPropertyName("post_id")]
        public string? PostId { get; set; }

        [JsonPropertyName("owner")]
        public OwnerData? Owner { get; set; }

        [JsonPropertyName("text_content")]
        public string? TextContent { get; set; }

        [JsonPropertyName("photos")]
        public List<string> Photos { get; set; } = new List<string>();

        [JsonPropertyName("comment_count")]
        public string? CommentCount { get; set; }

        [JsonPropertyName("like_count")]
        public string? LikeCount { get; set; }

        [JsonPropertyName("post_time")]
        public string? PostTime { get; set; }

        [JsonPropertyName("language_code")]
        public string? LanguageCode { get; set; }

        [JsonPropertyName("comments_url")]
        public string? CommentsUrl { get; set; }
    }
}
