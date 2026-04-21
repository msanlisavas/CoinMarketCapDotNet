using CoinMarketCapDotNet.Models.Cryptocurrency.Listing;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Trending.GainersLosers
{
    public class GainersLosersData : ListingBaseData
    {
        [JsonPropertyName("market_cap_by_total_supply")]
        public decimal? MarketCapByTotalSupply { get; set; }
    }
}
