using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Category.Query
{
    public class CategoryQueryParameters : QueryParameters
    {
        public void AddId(string id)
        {
            Add("id", id);
        }

        public void AddStart(int start)
        {
            Add("start", start.ToString());
        }

        public void AddLimit(int limit)
        {
            Add("limit", limit.ToString());
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
