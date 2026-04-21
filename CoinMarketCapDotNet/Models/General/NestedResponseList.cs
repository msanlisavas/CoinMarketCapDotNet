using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.General
{
    public class NestedResponseList<T>
    {
        [JsonPropertyName("data")]
        public DataContainer<T>? Data { get; set; }

        [JsonPropertyName("status")]
        public Status? Status { get; set; }
    }
}
