using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Key
{
    public class CurrentDayData
    {

        [JsonProperty("credits_used")]
        public double? CreditsUsed { get; set; }

        [JsonProperty("credits_left")]
        public double? CreditsLeft { get; set; }
    }
}
