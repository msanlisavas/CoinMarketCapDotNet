using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Key
{
    public class CurrentMinuteData
    {

        [JsonProperty("requests_made")]
        public double? RequestsMade { get; set; }

        [JsonProperty("requests_left")]
        public double? RequestsLeft { get; set; }
    }
}
