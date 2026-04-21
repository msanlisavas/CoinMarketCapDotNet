using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Content.Latest
{
    public class ContentAssetsData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }
    }
}
