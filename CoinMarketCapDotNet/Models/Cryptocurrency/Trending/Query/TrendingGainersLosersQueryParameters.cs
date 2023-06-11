using CoinMarketCapDotNet.Extensions;
using CoinMarketCapDotNet.Models.Enums;
using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Trending.Query
{
    public class TrendingGainersLosersQueryParameters : QueryParameters
    {
        public void AddStart(int start)
        {
            Add("start", start);
        }
        public void AddLimit(int limit)
        {
            Add("limit", limit);
        }
        public void AddTimePeriod(TimePeriodEnum timePeriod)
        {
            Add("time_period", timePeriod.GetEnumMemberValue());
        }
        public void AddConvert(string convert)
        {
            Add("convert", convert);
        }

        public void AddConvertId(string convertId)
        {
            Add("convert_id", convertId);
        }
        public void AddSort(SortTrendingGainersLosersEnum sort)
        {
            Add("sort", sort.GetEnumMemberValue());
        }
        public void AddSortDir(SortDirectionEnum sortDir)
        {
            Add("sort_dir", sortDir.GetEnumMemberValue());
        }
    }
}
