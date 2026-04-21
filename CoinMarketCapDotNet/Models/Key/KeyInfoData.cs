using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Key
{
    public class KeyInfoData
    {
        [JsonPropertyName("plan")]
        public PlanData? Plan { get; set; }
        [JsonPropertyName("usage")]
        public UsageData? Usage { get; set; }
    }
}
