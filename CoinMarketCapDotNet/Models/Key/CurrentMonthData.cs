using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Key
{
    public class CurrentMonthData
    {

        [JsonProperty("credits_used")]
        public double? CreditsUsed { get; set; }

        [JsonProperty("credits_left")]
        public double? CreditsLeft { get; set; }
    }
}
