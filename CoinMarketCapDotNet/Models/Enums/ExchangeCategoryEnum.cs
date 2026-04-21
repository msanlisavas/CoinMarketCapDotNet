using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum ExchangeCategoryEnum
    {
        [JsonPropertyName("all")]
        All,
        [JsonPropertyName("spot")]
        Spot,
        [JsonPropertyName("derivatives")]
        Derivatives,
        [JsonPropertyName("dex")]
        Dex,
        [JsonPropertyName("lending")]
        Lending
    }
}
