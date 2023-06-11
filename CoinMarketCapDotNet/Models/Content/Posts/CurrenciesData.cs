using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Content.Posts
{
    public class CurrenciesData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
    }
}
