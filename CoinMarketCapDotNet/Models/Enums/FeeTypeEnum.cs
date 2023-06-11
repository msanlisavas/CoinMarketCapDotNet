using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum FeeTypeEnum
    {
        [EnumMember(Value = "all")]
        All,

        [EnumMember(Value = "percentage")]
        Percentage,

        [EnumMember(Value = "no-fees")]
        NoFees,

        [EnumMember(Value = "transactional-mining")]
        TransactionalMining,

        [EnumMember(Value = "unknown")]
        Unknown
    }
}
