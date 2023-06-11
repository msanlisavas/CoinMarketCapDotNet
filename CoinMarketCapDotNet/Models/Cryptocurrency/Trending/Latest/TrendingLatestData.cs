using CoinMarketCapDotNet.Models.Cryptocurrency.Listing;
using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Trending.Latest
{
    public class TrendingLatestData : ListingBaseData
    {
        [JsonProperty("is_fiat")]
        public int? IsFiat { get; set; }

        [JsonProperty("self_reported_circulating_supply")]
        public decimal? SelfReportedCirculatingSupply { get; set; }

        [JsonProperty("self_reported_market_cap")]
        public decimal? SelfReportedMarketCap { get; set; }

        [JsonProperty("is_active")]
        public bool? IsActive { get; set; }

        [JsonProperty("market_cap_by_total_supply")]
        public decimal? MarketCapByTotalSupply { get; set; }
    }
}
