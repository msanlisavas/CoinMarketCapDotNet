using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Categories
{
    public class CategoriesData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("num_tokens")]
        public int? NumTokens { get; set; }

        [JsonPropertyName("avg_price_change")]
        public double? AveragePriceChange { get; set; }

        [JsonPropertyName("market_cap")]
        public double? MarketCap { get; set; }

        [JsonPropertyName("market_cap_change")]
        public double? MarketCapChange { get; set; }

        [JsonPropertyName("volume")]
        public double? Volume { get; set; }

        [JsonPropertyName("volume_change")]
        public double? VolumeChange { get; set; }

        [JsonPropertyName("last_updated")]
        // The sandbox API returns a string or even random numbers 5404, the production API returns a DateTime hopefully
        public string LastUpdated { get; set; }
    }
}
