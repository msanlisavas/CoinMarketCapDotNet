using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Content.Posts
{
    public class CurrenciesData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }
    }
}
