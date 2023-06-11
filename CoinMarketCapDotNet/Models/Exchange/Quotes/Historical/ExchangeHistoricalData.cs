using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Exchange.Quotes.Historical
{
    public class ExchangeHistoricalData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("quotes")]
        public List<ExchangeHistoricalQuotesData> Quotes { get; set; }
    }
}
