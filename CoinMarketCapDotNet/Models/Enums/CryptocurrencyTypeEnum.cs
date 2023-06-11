using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum CryptocurrencyTypeEnum
    {
        [EnumMember(Value = "all")]
        All,
        [EnumMember(Value = "coins")]
        Coins,
        [EnumMember(Value = "tokens")]
        Tokens
    }
}
