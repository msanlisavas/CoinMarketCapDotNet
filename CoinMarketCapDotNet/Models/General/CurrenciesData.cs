using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.General
{
    public class CurrenciesData
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
    }
}
