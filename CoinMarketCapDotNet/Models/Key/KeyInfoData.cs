using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Key
{
    public class KeyInfoData
    {
        [JsonProperty("plan")]
        public PlanData Plan { get; set; }
        [JsonProperty("usage")]
        public UsageData Usage { get; set; }
    }
}
