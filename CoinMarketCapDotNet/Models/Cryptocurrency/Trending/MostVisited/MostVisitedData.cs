using CoinMarketCapDotNet.Models.Cryptocurrency.Listing;
using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Trending.MostVisited
{
    public class MostVisitedData : ListingBaseData
    {
        [JsonProperty("market_cap_by_total_supply")]
        public decimal? MarketCapByTotalSupply { get; set; }
    }
}
