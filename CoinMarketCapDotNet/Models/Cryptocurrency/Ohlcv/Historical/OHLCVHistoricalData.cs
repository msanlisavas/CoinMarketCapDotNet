using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Ohlcv.Historical
{
    public class OHLCVHistoricalData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("quotes")]
        public List<OHLCVHistoricalQuoteData> Quotes { get; set; }
    }
}
