using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum SortDirectionEnum
    {
        [EnumMember(Value = "asc")]
        Ascending,

        [EnumMember(Value = "desc")]
        Descending
    }
}
