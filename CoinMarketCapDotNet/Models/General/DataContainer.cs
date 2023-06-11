using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.General
{
    public class DataContainer<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
}
