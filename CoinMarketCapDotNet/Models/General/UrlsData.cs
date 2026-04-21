using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.General
{
    public class UrlsData
    {
        [JsonPropertyName("website")]
        public List<string> Website { get; set; }

        [JsonPropertyName("technical_doc")]
        public List<string> TechnicalDoc { get; set; }

        [JsonPropertyName("twitter")]
        public List<object> Twitter { get; set; }

        [JsonPropertyName("reddit")]
        public List<string> Reddit { get; set; }

        [JsonPropertyName("message_board")]
        public List<string> MessageBoard { get; set; }

        [JsonPropertyName("announcement")]
        public List<object> Announcement { get; set; }

        [JsonPropertyName("chat")]
        public List<object> Chat { get; set; }

        [JsonPropertyName("explorer")]
        public List<string> Explorer { get; set; }

        [JsonPropertyName("source_code")]
        public List<string> SourceCode { get; set; }
    }
}
