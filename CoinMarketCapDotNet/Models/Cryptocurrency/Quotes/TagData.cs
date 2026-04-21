using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Quotes
{
    public class TagData
    {
        [JsonPropertyName("slug")]
        public string? Slug { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("category")]
        public string? Category { get; set; }
    }
}
