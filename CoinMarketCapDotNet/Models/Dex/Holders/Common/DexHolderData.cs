using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinMarketCapDotNet.Models.Dex.Holders.Common
{
    public class DexHolderData
    {
        [JsonPropertyName("wallet_address")]
        public string? WalletAddress { get; set; }

        [JsonPropertyName("network_slug")]
        public string? NetworkSlug { get; set; }

        [JsonPropertyName("token_address")]
        public string? TokenAddress { get; set; }

        [JsonPropertyName("balance")]
        public double? Balance { get; set; }

        [JsonPropertyName("balance_usd")]
        public double? BalanceUsd { get; set; }

        [JsonPropertyName("holding_ratio")]
        public double? HoldingRatio { get; set; }

        [JsonPropertyName("realized_pnl_percent")]
        public double? RealizedPnlPercent { get; set; }

        [JsonPropertyName("cost_basis_usd")]
        public double? CostBasisUsd { get; set; }

        [JsonPropertyName("bought_volume_usd")]
        public double? BoughtVolumeUsd { get; set; }

        [JsonPropertyName("sold_volume_usd")]
        public double? SoldVolumeUsd { get; set; }

        [JsonPropertyName("transaction_count")]
        public int? TransactionCount { get; set; }

        [JsonPropertyName("first_buy_time")]
        public DateTime? FirstBuyTime { get; set; }

        [JsonPropertyName("last_activity_time")]
        public DateTime? LastActivityTime { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new List<string>();
    }
}
