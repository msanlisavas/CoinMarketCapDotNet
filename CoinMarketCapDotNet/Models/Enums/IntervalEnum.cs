using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum IntervalEnum
    {
        [EnumMember(Value = "daily")]
        Daily,
        [EnumMember(Value = "hourly")]
        Hourly,
        [EnumMember(Value = "weekly")]
        Weekly,
        [EnumMember(Value = "monthly")]
        Monthly,
        [EnumMember(Value = "yearly")]
        Yearly,
        [EnumMember(Value = "5m")]
        FiveMinutes,
        [EnumMember(Value = "10m")]
        TenMinutes,
        [EnumMember(Value = "15m")]
        FifteenMinutes,
        [EnumMember(Value = "30m")]
        ThirtyMinutes,
        [EnumMember(Value = "45m")]
        FortyFiveMinutes,
        [EnumMember(Value = "1h")]
        OneHour,
        [EnumMember(Value = "2h")]
        TwoHours,
        [EnumMember(Value = "3h")]
        ThreeHours,
        [EnumMember(Value = "4h")]
        FourHours,
        [EnumMember(Value = "6h")]
        SixHours,
        [EnumMember(Value = "12h")]
        TwelveHours,
        [EnumMember(Value = "1d")]
        OneDay,
        [EnumMember(Value = "2d")]
        TwoDays,
        [EnumMember(Value = "3d")]
        ThreeDays,
        [EnumMember(Value = "7d")]
        SevenDays,
        [EnumMember(Value = "14d")]
        FourteenDays,
        [EnumMember(Value = "15d")]
        FifteenDays,
        [EnumMember(Value = "30d")]
        ThirtyDays,
        [EnumMember(Value = "60d")]
        SixtyDays,
        [EnumMember(Value = "90d")]
        NinetyDays,
        [EnumMember(Value = "365d")]
        ThreeHundredSixtyFiveDays
    }

}
