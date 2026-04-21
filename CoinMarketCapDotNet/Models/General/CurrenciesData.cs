using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.General
{
    public class CurrenciesData
    {
        [JsonPropertyName("id")]
        public double? Id { get; set; }

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }
    }
}
