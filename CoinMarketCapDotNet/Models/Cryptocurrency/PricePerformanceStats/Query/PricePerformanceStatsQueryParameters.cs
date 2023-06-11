using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.PricePerformanceStats.Query
{
    public class PricePerformanceStatsQueryParameters : QueryParameters
    {
        public void AddId(string id)
        {
            Add("id", id);
        }

        public void AddSlug(string slug)
        {
            Add("slug", slug);
        }

        public void AddSymbol(string symbol)
        {
            Add("symbol", symbol);
        }

        public void AddTimePeriod(string timePeriod)
        {
            Add("time_period", timePeriod);
        }

        public void AddConvert(string convert)
        {
            Add("convert", convert);
        }

        public void AddConvertId(string convertId)
        {
            Add("convert_id", convertId);
        }

        public void AddSkipInvalid(bool skipInvalid)
        {
            Add("skip_invalid", skipInvalid.ToString());
        }
    }

}
