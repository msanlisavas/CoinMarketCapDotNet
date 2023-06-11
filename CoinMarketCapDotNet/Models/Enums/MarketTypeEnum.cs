using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum MarketTypeEnum
    {
        [JsonProperty("all")]
        All,
        [JsonProperty("fees")]
        Fees,
        [JsonProperty("no_fees")]
        NoFees,

    }
}
