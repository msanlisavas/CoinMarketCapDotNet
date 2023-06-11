using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum ExchangeCategoryEnum
    {
        [JsonProperty("all")]
        All,
        [JsonProperty("spot")]
        Spot,
        [JsonProperty("derivatives")]
        Derivatives,
        [JsonProperty("dex")]
        Dex,
        [JsonProperty("lending")]
        Lending
    }
}
