using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Exchange.Quotes.Historical
{
    public class ExchangeHistoricalData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }
        [JsonPropertyName("quotes")]
        public List<ExchangeHistoricalQuotesData> Quotes { get; set; }
    }
}
