using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum NewsTypeEnum
    {
        [EnumMember(Value = "all")]
        All,
        [EnumMember(Value = "news")]
        News,
        [EnumMember(Value = "community")]
        Community,
        [EnumMember(Value = "alexandria")]
        Alexandria
    }
}
