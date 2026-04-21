using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Key
{
    public class CurrentMinuteData
    {

        [JsonPropertyName("requests_made")]
        public double? RequestsMade { get; set; }

        [JsonPropertyName("requests_left")]
        public double? RequestsLeft { get; set; }
    }
}
