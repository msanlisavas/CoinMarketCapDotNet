using System;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Token.Transactions
{
    public class DexTokenTransactionData
    {
        [JsonPropertyName("transaction_hash")]
        public string? TransactionHash { get; set; }

        [JsonPropertyName("block_number")]
        public long? BlockNumber { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("amount_usd")]
        public double? AmountUsd { get; set; }

        [JsonPropertyName("from_address")]
        public string? FromAddress { get; set; }

        [JsonPropertyName("to_address")]
        public string? ToAddress { get; set; }
    }
}
