using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Quotes.Historical
{
    public class QuotesHistoricalData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("is_active")]
        public int? IsActive { get; set; }

        [JsonProperty("is_fiat")]
        public int? IsFiat { get; set; }

        [JsonProperty("quotes")]
        public List<HistoricalQuoteData> Quotes { get; set; }
    }
}
