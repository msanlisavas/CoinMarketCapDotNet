using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Quotes
{
    public class TagData
    {
        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
    }
}
