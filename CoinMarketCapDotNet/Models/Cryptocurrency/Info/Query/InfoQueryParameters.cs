using CoinMarketCapDotNet.Models.General;
using System.Collections.Generic;
using System.Linq;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Info.Query
{
    public class InfoQueryParameters : QueryParameters
    {
        public void Add(string key, IEnumerable<string> values)
        {
            if (values != null && values.Any())
            {
                Add(key, string.Join(",", values));
            }
        }

    }
}
