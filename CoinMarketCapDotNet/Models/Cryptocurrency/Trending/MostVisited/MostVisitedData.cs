using CoinMarketCapDotNet.Models.Cryptocurrency.Listing;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Trending.MostVisited
{
    public class MostVisitedData : ListingBaseData
    {
        [JsonPropertyName("market_cap_by_total_supply")]
        public decimal? MarketCapByTotalSupply { get; set; }
    }
}
