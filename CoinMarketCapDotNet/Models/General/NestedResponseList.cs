using Newtonsoft.Json;

namespace CoinMarketCapDotNet.Models.General
{
    public class NestedResponseList<T>
    {
        [JsonProperty("data")]
        public DataContainer<T> Data { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }
    }
}
