using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.General
{
    public class ExchangeData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("notice")]
        public string? Notice { get; set; }
    }
}
