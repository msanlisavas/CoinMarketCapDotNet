using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Key
{
    public class CurrentMonthData
    {

        [JsonPropertyName("credits_used")]
        public double? CreditsUsed { get; set; }

        [JsonPropertyName("credits_left")]
        public double? CreditsLeft { get; set; }
    }
}
