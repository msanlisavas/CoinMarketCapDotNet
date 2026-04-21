using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Index.Historical.Query
{
    public class IndexHistoricalQueryParameters : QueryParameters
    {
        public IndexHistoricalQueryParameters(string? interval = null, int? start = null, int? end = null, int? count = null)
        {
            if (!string.IsNullOrWhiteSpace(interval)) Add("interval", interval);
            if (start.HasValue) Add("start", start.Value.ToString());
            if (end.HasValue) Add("end", end.Value.ToString());
            if (count.HasValue) Add("count", count.Value.ToString());
        }
    }
}
