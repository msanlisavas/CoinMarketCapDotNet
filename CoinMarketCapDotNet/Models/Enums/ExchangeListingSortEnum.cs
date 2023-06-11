using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum ExchangeListingSortEnum
    {
        [EnumMember(Value = "volume_24h")]
        Volume24h,

        [EnumMember(Value = "name")]
        Name,

        [EnumMember(Value = "volume_24h_adjusted")]
        Volume24hAdjusted,

        [EnumMember(Value = "exchange_score")]
        ExchangeScore,
    }
}
