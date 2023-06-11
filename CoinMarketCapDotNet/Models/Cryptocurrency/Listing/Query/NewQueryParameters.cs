using CoinMarketCapDotNet.Extensions;
using CoinMarketCapDotNet.Models.Enums;
using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Listing.Query
{
    public class NewQueryParameters : QueryParameters
    {
        public void AddStart(int start)
        {
            Add("start", start);
        }
        public void AddLimit(int limit)
        {
            Add("limit", limit);
        }
        public void AddConvert(string convert)
        {
            Add("convert", convert);
        }

        public void AddConvertId(string convertId)
        {
            Add("convert_id", convertId);
        }

        public void AddSortDir(SortDirectionEnum sortDir)
        {
            Add("sort_dir", sortDir.GetEnumMemberValue());
        }
    }
}
