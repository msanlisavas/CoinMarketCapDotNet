using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum SortMapEnum
    {
        [EnumMember(Value = "cmc_rank")]
        CMCRank,

        [EnumMember(Value = "id")]
        Id
    }
}
