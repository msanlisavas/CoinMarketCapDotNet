using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum CategoryEnum
    {
        [EnumMember(Value = "all")]
        All,

        [EnumMember(Value = "spot")]
        Spot,

        [EnumMember(Value = "derivatives")]
        Derivatives,

        [EnumMember(Value = "otc")]
        OTC,

        [EnumMember(Value = "perpetual")]
        Perpetual,
        [EnumMember(Value = "futures")]
        Futures,
    }
}
