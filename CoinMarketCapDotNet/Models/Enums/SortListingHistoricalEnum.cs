using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum SortListingHistoricalEnum
    {
        [EnumMember(Value = "cmc_rank")]
        CMCRank,

        [EnumMember(Value = "name")]
        Name,

        [EnumMember(Value = "symbol")]
        Symbol,

        [EnumMember(Value = "market_cap")]
        MarketCap,

        [EnumMember(Value = "price")]
        Price,

        [EnumMember(Value = "circulating_supply")]
        CirculatingSupply,

        [EnumMember(Value = "total_supply")]
        TotalSupply,

        [EnumMember(Value = "max_supply")]
        MaxSupply,

        [EnumMember(Value = "num_market_pairs")]
        NumMarketPairs,

        [EnumMember(Value = "volume_24h")]
        Volume24h,

        [EnumMember(Value = "percent_change_1h")]
        PercentChange1h,

        [EnumMember(Value = "percent_change_24h")]
        PercentChange24h,

        [EnumMember(Value = "percent_change_7d")]
        PercentChange7d
    }
}
