using CoinMarketCapDotNet.Models.General;

namespace CoinMarketCapDotNet.Models.Exchange.Assets.Query
{
    public class AssetsQueryParameters : QueryParameters
    {
        public void AddId(string id)
        {
            Add("id", id);
        }
    }
}
