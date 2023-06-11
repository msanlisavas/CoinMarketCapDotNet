using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Blockchain.Statistics.Query
{
    public class BlockchainQueryParameters : QueryParameters
    {
        public void AddId(string ids)
        {
            Add("id", ids);
        }
        public void AddSymbol(string symbols)
        {
            Add("symbol", symbols);
        }
        public void AddSlug(string slugs)
        {
            Add("slug", slugs);
        }
    }
}
