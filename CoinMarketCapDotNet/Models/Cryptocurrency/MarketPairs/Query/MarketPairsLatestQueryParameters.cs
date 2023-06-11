using CoinMarketCapDotNet.Extensions;
using CoinMarketCapDotNet.Models.Enums;
using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.MarketPairs.Query
{
    public class MarketPairsQueryParameters : QueryParameters
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

        public void AddStart(int start)
        {
            Add("start", start.ToString());
        }

        public void AddLimit(int limit)
        {
            Add("limit", limit.ToString());
        }

        public void AddSortDir(SortDirectionEnum sortDir)
        {
            Add("sort_dir", sortDir.GetEnumMemberValue());
        }

        public void AddSort(SortMarketPairsLatestEnum sort)
        {
            Add("sort", sort.GetEnumMemberValue());
        }

        public void AddAux(string aux)
        {
            Add("aux", aux);
        }

        public void AddMatchedId(string matchedId)
        {
            Add("matched_id", matchedId);
        }

        public void AddMatchedSymbol(string matchedSymbol)
        {
            Add("matched_symbol", matchedSymbol);
        }

        public void AddCategory(CategoryEnum category)
        {
            Add("category", category.GetEnumMemberValue());
        }

        public void AddFeeType(FeeTypeEnum feeType)
        {
            Add("fee_type", feeType.GetEnumMemberValue());
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
