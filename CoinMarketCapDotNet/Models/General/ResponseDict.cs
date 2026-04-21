using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.General
{
    public class ResponseDict<T>
    {
        [JsonPropertyName("data")]
        public Dictionary<string, T>? Data { get; set; }

        [JsonPropertyName("status")]
        public Status? Status { get; set; }
    }
}
