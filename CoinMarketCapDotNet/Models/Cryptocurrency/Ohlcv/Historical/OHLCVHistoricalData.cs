using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Ohlcv.Historical
{
    public class OHLCVHistoricalData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("quotes")]
        public List<OHLCVHistoricalQuoteData> Quotes { get; set; }
    }
}
