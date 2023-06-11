using CoinMarketCapDotNet.Models.General;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.Cryptocurrency.Ohlcv
{
    public class OHLCVHistoricalQuoteData
    {
        [JsonProperty("time_open")]
        public string TimeOpen { get; set; }

        [JsonProperty("time_close")]
        public string TimeClose { get; set; }

        [JsonProperty("time_high")]
        public string TimeHigh { get; set; }

        [JsonProperty("time_low")]
        public string TimeLow { get; set; }

        [JsonProperty("quote")]
        public Dictionary<string, MarketQuoteData> Quotes { get; set; }
    }
}
