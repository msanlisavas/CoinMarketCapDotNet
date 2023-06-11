using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Categories.Query
{
    public class CategoriesQueryParameters : QueryParameters
    {
        public void AddStart(int start)
        {
            Add("start", start);
        }

        public void AddLimit(int limit)
        {
            Add("limit", limit);
        }

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
    }
}
