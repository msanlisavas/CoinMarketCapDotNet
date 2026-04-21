using System.Text.Json.Serialization;
using System;

namespace CoinMarketCapDotNet.Models.Blockchain.Statistics
{
    public class BlockchainData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("block_reward_static")]
        public double? BlockRewardStatic { get; set; }

        [JsonPropertyName("consensus_mechanism")]
        public string ConsensusMechanism { get; set; }

        [JsonPropertyName("difficulty")]
        public string Difficulty { get; set; }

        [JsonPropertyName("hashrate_24h")]
        public string Hashrate24h { get; set; }

        [JsonPropertyName("pending_transactions")]
        public int PendingTransactions { get; set; }

        [JsonPropertyName("reduction_rate")]
        public string ReductionRate { get; set; }

        [JsonPropertyName("total_blocks")]
        public int? TotalBlocks { get; set; }

        [JsonPropertyName("total_transactions")]
        public string TotalTransactions { get; set; }

        [JsonPropertyName("tps_24h")]
        public double? Tps24h { get; set; }

        [JsonPropertyName("first_block_timestamp")]
        public DateTime? FirstBlockTimestamp { get; set; }
    }
}
