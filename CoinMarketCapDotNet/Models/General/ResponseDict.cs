using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.General
{
    public class ResponseDict<T>
    {
        [JsonProperty("data")]
        public Dictionary<string, T> Data { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }
    }
}
