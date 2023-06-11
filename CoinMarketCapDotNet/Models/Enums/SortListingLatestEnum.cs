using System.Runtime.Serialization;

namespace CoinMarketCapDotNet.Models.Enums
{
    public enum SortListingLatestEnum
    {
        [EnumMember(Value = "market_cap")]
        MarketCap,

        [EnumMember(Value = "name")]
        Name,

        [EnumMember(Value = "symbol")]
        Symbol,

        [EnumMember(Value = "date_added")]
        DateAdded,

        [EnumMember(Value = "market_cap_strict")]
        MarketCapStrict,

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
        PercentChange7d,

        [EnumMember(Value = "market_cap_by_total_supply_strict")]
        MarketCapByTotalSupplyStrict,

        [EnumMember(Value = "volume_7d")]
        Volume7d,

        [EnumMember(Value = "volume_30d")]
        Volume30d
    }
}
