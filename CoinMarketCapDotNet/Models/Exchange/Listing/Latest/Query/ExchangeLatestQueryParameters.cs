using CoinMarketCapDotNet.Extensions;
using CoinMarketCapDotNet.Models.Enums;
using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Exchange.Listing.Latest.Query
{
    public class ExchangeLatestQueryParameters : QueryParameters
    {
        public void AddStart(int start)
        {
            Add("start", start.ToString());
        }

        public void AddLimit(int limit)
        {
            Add("limit", limit.ToString());
        }
        public void AddSort(ExchangeListingSortEnum sort)
        {
            Add("sort", sort.GetEnumMemberValue());
        }

        public void AddSortDir(SortDirectionEnum sortDir)
        {
            Add("sort_dir", sortDir.GetEnumMemberValue());
        }


        public void AddMarketType(MarketTypeEnum marketType)
        {
            Add("market_type", marketType.GetEnumMemberValue());
        }
        public void AddCategory(ExchangeCategoryEnum category)
        {
            Add("category", category.GetEnumMemberValue());
        }

        public void AddAux(string aux)
        {
            Add("aux", aux);
        }
        public void AddConvert(string convert)
        {
            Add("convert", convert);
        }

        public void AddConvertId(string convertId)
        {
            Add("convert_id", convertId);
        }
    }
}
