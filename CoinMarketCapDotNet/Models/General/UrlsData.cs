using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.General
{
    public class UrlsData
    {
        [JsonProperty("website")]
        public List<string> Website { get; set; }

        [JsonProperty("technical_doc")]
        public List<string> TechnicalDoc { get; set; }

        [JsonProperty("twitter")]
        public List<object> Twitter { get; set; }

        [JsonProperty("reddit")]
        public List<string> Reddit { get; set; }

        [JsonProperty("message_board")]
        public List<string> MessageBoard { get; set; }

        [JsonProperty("announcement")]
        public List<object> Announcement { get; set; }

        [JsonProperty("chat")]
        public List<object> Chat { get; set; }

        [JsonProperty("explorer")]
        public List<string> Explorer { get; set; }

        [JsonProperty("source_code")]
        public List<string> SourceCode { get; set; }
    }
}
