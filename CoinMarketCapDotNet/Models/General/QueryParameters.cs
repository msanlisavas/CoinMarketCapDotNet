using System.Collections.Generic;
using System.Linq;

namespace CoinMarketCapDotNet.Models.General
{
    public class QueryParameters
    {
        private readonly Dictionary<string, string> parameters = new Dictionary<string, string>();

        public void Add(string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                parameters.Add(key, value);
            }
        }

        public void Add(string key, bool? value)
        {
            if (value.HasValue)
            {
                parameters.Add(key, value.Value ? "true" : "false");
            }
        }

        public void Add(string key, int value)
        {
            if (value != 0)
            {
                parameters.Add(key, value.ToString());
            }
        }

        public override string ToString()
        {
            return string.Join("&", parameters.Select(p => $"{p.Key}={p.Value}"));
        }
    }
}
