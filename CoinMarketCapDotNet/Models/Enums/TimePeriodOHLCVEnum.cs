using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum TimePeriodOHLCVEnum
    {
        [EnumMember(Value = "hourly")]
        Hourly,
        [EnumMember(Value = "daily")]
        Daily


    }
}
