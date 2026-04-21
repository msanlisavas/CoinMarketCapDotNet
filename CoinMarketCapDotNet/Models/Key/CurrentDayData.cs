using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Key
{
    public class CurrentDayData
    {

        [JsonPropertyName("credits_used")]
        public double? CreditsUsed { get; set; }

        [JsonPropertyName("credits_left")]
        public double? CreditsLeft { get; set; }
    }
}
