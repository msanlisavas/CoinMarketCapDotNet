using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCapDotNet.Models.General
{
    public class ResponseList<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }
    }
}
