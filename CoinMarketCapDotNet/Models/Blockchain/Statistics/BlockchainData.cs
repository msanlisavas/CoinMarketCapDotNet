using Newtonsoft.Json;
using System;

namespace CoinMarketCapDotNet.Models.Blockchain.Statistics
{
    public class BlockchainData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("block_reward_static")]
        public double? BlockRewardStatic { get; set; }

        [JsonProperty("consensus_mechanism")]
        public string ConsensusMechanism { get; set; }

        [JsonProperty("difficulty")]
        public string Difficulty { get; set; }

        [JsonProperty("hashrate_24h")]
        public string Hashrate24h { get; set; }

        [JsonProperty("pending_transactions")]
        public int PendingTransactions { get; set; }

        [JsonProperty("reduction_rate")]
        public string ReductionRate { get; set; }

        [JsonProperty("total_blocks")]
        public int? TotalBlocks { get; set; }

        [JsonProperty("total_transactions")]
        public string TotalTransactions { get; set; }

        [JsonProperty("tps_24h")]
        public double? Tps24h { get; set; }

        [JsonProperty("first_block_timestamp")]
        public DateTime? FirstBlockTimestamp { get; set; }
    }
}
