using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum TimePeriodEnum
    {

        [EnumMember(Value = "1h")]
        Hourly,

        [EnumMember(Value = "24h")]
        Daily,

        [EnumMember(Value = "30d")]
        Monthly,

        [EnumMember(Value = "7d")]
        Weekly
    }
}
