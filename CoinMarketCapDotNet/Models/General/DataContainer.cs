using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.General
{
    public class DataContainer<T>
    {
        [JsonPropertyName("data")]
        public List<T> Data { get; set; }
    }
}
