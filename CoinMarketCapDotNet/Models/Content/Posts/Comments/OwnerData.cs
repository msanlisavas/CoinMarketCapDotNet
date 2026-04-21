using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Content.Posts.Comments
{
    public class OwnerData
    {
        [JsonPropertyName("nickname")]
        public string Nickname { get; set; }

        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; }
    }
}
