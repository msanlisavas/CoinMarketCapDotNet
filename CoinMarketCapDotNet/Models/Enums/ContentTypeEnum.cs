using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum ContentTypeEnum
    {
        [EnumMember(Value = "all")]
        All,
        [EnumMember(Value = "news")]
        News,
        [EnumMember(Value = "video")]
        Video,
        [EnumMember(Value = "audio")]
        Audio
    }
}
