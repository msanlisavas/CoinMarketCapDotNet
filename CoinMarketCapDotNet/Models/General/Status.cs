using System.Text.Json.Serialization;
using System;

namespace CoinMarketCapDotNet.Models.General
{
    public class Status
    {
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("error_message")]
        public string? ErrorMessage { get; set; }

        [JsonPropertyName("elapsed")]
        public int Elapsed { get; set; }

        [JsonPropertyName("credit_count")]
        public int CreditCount { get; set; }
        [JsonPropertyName("notice")]
        public string? Notice { get; set; }
    }
}
