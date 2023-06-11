using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum TagEnum
    {
        [EnumMember(Value = "all")]
        All,
        [EnumMember(Value = "defi")]
        Defi,
        [EnumMember(Value = "filesharing")]
        Filesharing,
    }
}
