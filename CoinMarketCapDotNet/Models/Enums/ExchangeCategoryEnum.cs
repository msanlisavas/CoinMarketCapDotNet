using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum ExchangeCategoryEnum
    {
        [EnumMember(Value = "all")]
        All,
        [EnumMember(Value = "spot")]
        Spot,
        [EnumMember(Value = "derivatives")]
        Derivatives,
        [EnumMember(Value = "dex")]
        Dex,
        [EnumMember(Value = "lending")]
        Lending
    }
}
