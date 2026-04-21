using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum MarketTypeEnum
    {
        [EnumMember(Value = "all")]
        All,
        [EnumMember(Value = "fees")]
        Fees,
        [EnumMember(Value = "no_fees")]
        NoFees,
    }
}
