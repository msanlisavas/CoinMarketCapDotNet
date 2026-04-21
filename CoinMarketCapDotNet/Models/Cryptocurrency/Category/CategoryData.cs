using CoinMarketCapDotNet.Models.General;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Category
{
    public class CategoryData
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("num_tokens")]
        public int? NumTokens { get; set; }

        [JsonPropertyName("avg_price_change")]
        public double? AvgPriceChange { get; set; }

        [JsonPropertyName("market_cap")]
        public double? MarketCap { get; set; }

        [JsonPropertyName("market_cap_change")]
        public double? MarketCapChange { get; set; }

        [JsonPropertyName("volume")]
        public double? Volume { get; set; }

        [JsonPropertyName("volume_change")]
        public double? VolumeChange { get; set; }

        [JsonPropertyName("coins")]
        public List<CoinsData> Coins { get; set; } = new List<CoinsData>();
    }
}
