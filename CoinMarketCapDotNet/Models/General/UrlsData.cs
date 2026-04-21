using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.General
{
    public class UrlsData
    {
        [JsonPropertyName("website")]
        public List<string> Website { get; set; } = new List<string>();

        [JsonPropertyName("technical_doc")]
        public List<string> TechnicalDoc { get; set; } = new List<string>();

        [JsonPropertyName("twitter")]
        public List<object> Twitter { get; set; } = new List<object>();

        [JsonPropertyName("reddit")]
        public List<string> Reddit { get; set; } = new List<string>();

        [JsonPropertyName("message_board")]
        public List<string> MessageBoard { get; set; } = new List<string>();

        [JsonPropertyName("announcement")]
        public List<object> Announcement { get; set; } = new List<object>();

        [JsonPropertyName("chat")]
        public List<object> Chat { get; set; } = new List<object>();

        [JsonPropertyName("explorer")]
        public List<string> Explorer { get; set; } = new List<string>();

        [JsonPropertyName("source_code")]
        public List<string> SourceCode { get; set; } = new List<string>();
    }
}
