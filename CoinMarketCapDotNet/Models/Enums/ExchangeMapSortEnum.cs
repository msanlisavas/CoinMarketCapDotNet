using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum ExchangeMapSortEnum
    {
        [EnumMember(Value = "volume_24h")]
        Volume24h,

        [EnumMember(Value = "id")]
        Id
    }
}
