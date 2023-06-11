using CoinMarketCapDotNet.Models.Cryptocurrency.Listing;
using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Trending.GainersLosers
{
    public class GainersLosersData : ListingBaseData
    {
        [JsonProperty("market_cap_by_total_supply")]
        public decimal? MarketCapByTotalSupply { get; set; }
    }
}
