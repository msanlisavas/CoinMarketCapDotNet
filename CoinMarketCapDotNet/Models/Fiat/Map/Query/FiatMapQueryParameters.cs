using CoinMarketCapDotNet.Extensions;
using CoinMarketCapDotNet.Models.Enums;
using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Fiat.Map.Query
{
    public class FiatMapQueryParameters : QueryParameters
    {
        public void AddStart(int start)
        {
            Add("start", start);
        }
        public void AddLimit(int limit)
        {
            Add("limit", limit);
        }
        public void AddSort(SortFiatMapEnum sort)
        {
            Add("sort", sort.GetEnumMemberValue());
        }
        public void AddIncludeMetals(bool include_metals)
        {
            Add("include_metals", include_metals);
        }

    }
}
