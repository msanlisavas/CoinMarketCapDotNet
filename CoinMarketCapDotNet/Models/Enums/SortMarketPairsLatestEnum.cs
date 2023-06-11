using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum SortMarketPairsLatestEnum
    {
        [EnumMember(Value = "volume_24h_strict")]
        Volume24hStrict,

        [EnumMember(Value = "cmc_rank")]
        CmcRank,

        [EnumMember(Value = "cmc_rank_advanced")]
        CmcRankAdvanced,

        [EnumMember(Value = "effective_liquidity")]
        EffectiveLiquidity,

        [EnumMember(Value = "market_score")]
        MarketScore,

        [EnumMember(Value = "market_reputation")]
        MarketReputation
    }
}
