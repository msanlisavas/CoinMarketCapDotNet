using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.General
{
    public class Response<T>
    {
        [JsonPropertyName("status")]
        public Status? Status { get; set; }

        [JsonPropertyName("data")]
        public T? Data { get; set; }
    }
}
