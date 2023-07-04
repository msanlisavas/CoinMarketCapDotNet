using Newtonsoft.Json;

namespace CoinMarketCapSdk.Models.Cryptocurrency.Categories
{
    public class CategoriesData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("num_tokens")]
        public int? NumTokens { get; set; }

        [JsonProperty("avg_price_change")]
        public double? AveragePriceChange { get; set; }

        [JsonProperty("market_cap")]
        public double? MarketCap { get; set; }

        [JsonProperty("market_cap_change")]
        public double? MarketCapChange { get; set; }

        [JsonProperty("volume")]
        public double? Volume { get; set; }

        [JsonProperty("volume_change")]
        public double? VolumeChange { get; set; }

        [JsonProperty("last_updated")]
        // The sandbox API returns a string or even random numbers 5404, the production API returns a DateTime hopefully
        public string LastUpdated { get; set; }
    }
}
