using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Content.Posts.Comments
{
    public class OwnerData
    {
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
    }
}
