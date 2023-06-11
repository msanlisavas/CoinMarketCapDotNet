using CoinMarketCapDotNet.Extensions;
using CoinMarketCapDotNet.Models.Enums;
using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Airdrops.Query
{
    public class AirdropsQueryParameters : QueryParameters
    {
        public void AddStart(int start)
        {
            Add("start", start);
        }

        public void AddLimit(int limit)
        {
            Add("limit", limit);
        }

        public void AddStatus(StatusEnum status)
        {
            Add("status", status.GetEnumMemberValue());
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
