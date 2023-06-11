using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Community.Trending.Query
{
    public class TrendingTokenQueryParameters : QueryParameters
    {
        public void AddLimit(int limit)
        {
            Add("limit", limit);
        }
    }
}
