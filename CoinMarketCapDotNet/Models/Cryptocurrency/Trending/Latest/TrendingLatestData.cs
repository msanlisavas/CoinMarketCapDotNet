using CoinMarketCapDotNet.Models.Cryptocurrency.Listing;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Trending.Latest
{
    public class TrendingLatestData : ListingBaseData
    {
        [JsonPropertyName("is_fiat")]
        public int? IsFiat { get; set; }

        [JsonPropertyName("self_reported_circulating_supply")]
        public decimal? SelfReportedCirculatingSupply { get; set; }

        [JsonPropertyName("self_reported_market_cap")]
        public decimal? SelfReportedMarketCap { get; set; }

        [JsonPropertyName("is_active")]
        public bool? IsActive { get; set; }

        [JsonPropertyName("market_cap_by_total_supply")]
        public decimal? MarketCapByTotalSupply { get; set; }
    }
}
