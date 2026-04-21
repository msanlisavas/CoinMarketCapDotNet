using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Quotes.Historical
{
    public class QuotesHistoricalData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("is_active")]
        public int? IsActive { get; set; }

        [JsonPropertyName("is_fiat")]
        public int? IsFiat { get; set; }

        [JsonPropertyName("quotes")]
        public List<HistoricalQuoteData> Quotes { get; set; } = new List<HistoricalQuoteData>();
    }
}
