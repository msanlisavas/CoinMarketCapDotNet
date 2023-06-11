using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum StatusEnum
    {
        [EnumMember(Value = "ENDED")]
        Ended,
        [EnumMember(Value = "ONGOING")]
        Ongoing,
        [EnumMember(Value = "UPCOMING")]
        Upcoming
    }
}
