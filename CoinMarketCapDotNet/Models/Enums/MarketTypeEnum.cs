using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum MarketTypeEnum
    {
        [JsonPropertyName("all")]
        All,
        [JsonPropertyName("fees")]
        Fees,
        [JsonPropertyName("no_fees")]
        NoFees,

    }
}
