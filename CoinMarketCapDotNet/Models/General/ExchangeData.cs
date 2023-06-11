using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.General
{
    public class ExchangeData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("notice")]
        public string Notice { get; set; }
    }
}
