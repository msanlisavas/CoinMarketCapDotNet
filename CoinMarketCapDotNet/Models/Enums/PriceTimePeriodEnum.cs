using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum PriceTimePeriodEnum
    {
        [EnumMember(Value = "all_time")]
        AllTime,
        [EnumMember(Value = "yesterday")]
        Yesterday,
        [EnumMember(Value = "24h")]
        Daily,
        [EnumMember(Value = "7d")]
        Weekly,
        [EnumMember(Value = "30d")]
        Monthly,
        [EnumMember(Value = "90d")]
        ThreeMonthly,
        [EnumMember(Value = "365d")]
        Yearly,
    }
}
