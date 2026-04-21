using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.FearAndGreed.Historical.Query
{
    public class FearAndGreedHistoricalQueryParameters : QueryParameters
    {
        public FearAndGreedHistoricalQueryParameters(int? start = null, int? limit = null)
        {
            if (start.HasValue) Add("start", start.Value.ToString());
            if (limit.HasValue) Add("limit", limit.Value.ToString());
        }
    }
}
