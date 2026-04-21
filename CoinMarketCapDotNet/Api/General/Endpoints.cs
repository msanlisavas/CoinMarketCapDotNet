namespace CoinMarketCapDotNet.Api.General
{
    public static class Endpoints
    {
        public static class Version
        {
            public static string V1 { get; } = "v1";
            public static string V2 { get; } = "v2";
            public static string V3 { get; } = "v3";
            public static string V4 { get; } = "v4";
        }

        public static class Cryptocurrency
        {
            public static string Map { get; } = $"{Version.V1}/cryptocurrency/map";
            public static string Info { get; } = $"{Version.V2}/cryptocurrency/info";
            public static string Category { get; } = $"{Version.V1}/cryptocurrency/category";
            public static string Categories { get; } = $"{Version.V1}/cryptocurrency/categories";
            public static string Airdrops { get; } = $"{Version.V1}/cryptocurrency/airdrops";
            public static string Airdrop { get; } = $"{Version.V1}/cryptocurrency/airdrop";
            public static class Listing
            {
                public static string Latest { get; } = $"{Version.V1}/cryptocurrency/listings/latest";
                public static string Historical { get; } = $"{Version.V1}/cryptocurrency/listings/historical";
                public static string New { get; } = $"{Version.V1}/cryptocurrency/listings/new";
                public static string LatestV3 { get; } = $"{Version.V3}/cryptocurrency/listings/latest";
            }
            public static class Trending
            {
                public static string GainersLosers { get; } = $"{Version.V1}/cryptocurrency/trending/gainers-losers";
                public static string Latest { get; } = $"{Version.V1}/cryptocurrency/trending/latest";
                public static string MostVisited { get; } = $"{Version.V1}/cryptocurrency/trending/most-visited";
            }
            public static class MarketPairs
            {
                public static string Latest { get; } = $"{Version.V2}/cryptocurrency/market-pairs/latest";
            }
            public static class OHLCV
            {
                public static string Latest { get; } = $"{Version.V2}/cryptocurrency/ohlcv/latest";
                public static string Historical { get; } = $"{Version.V2}/cryptocurrency/ohlcv/historical";
            }
            public static class PricePerformanceStats
            {
                public static string Latest { get; } = $"{Version.V2}/cryptocurrency/price-performance-stats/latest";
            }
            public static class Quotes
            {
                public static string HistoricalV2 { get; } = $"{Version.V2}/cryptocurrency/quotes/historical";
                public static string Latest { get; } = $"{Version.V2}/cryptocurrency/quotes/latest";
                public static string HistoricalV3 { get; } = $"{Version.V3}/cryptocurrency/quotes/historical";
                public static string LatestV3 { get; } = $"{Version.V3}/cryptocurrency/quotes/latest";
            }
            // Add more endpoints for the Cryptocurrency category here
        }
        public static class Dex
        {
            public static class Token
            {
                public static string TrendingList { get; } = $"{Version.V1}/dex/tokens/trending/list";
                public static string BatchQuery { get; } = $"{Version.V1}/dex/tokens/batch-query";
                public static string BatchPrice { get; } = $"{Version.V1}/dex/token/price/batch";
                public static string NewList { get; } = $"{Version.V1}/dex/new/list";
                public static string MemeList { get; } = $"{Version.V1}/dex/meme/list";
                public static string GainerLoserList { get; } = $"{Version.V1}/dex/gainer-loser/list";
                public static string Detail { get; } = $"{Version.V1}/dex/token";
                public static string Price { get; } = $"{Version.V1}/dex/token/price";
                public static string Pools { get; } = $"{Version.V1}/dex/token/pools";
                public static string Liquidity { get; } = $"{Version.V1}/dex/token-liquidity/query";
                public static string Transactions { get; } = $"{Version.V1}/dex/tokens/transactions";
                public static string Security { get; } = $"{Version.V1}/dex/security/detail";
                public static string Search { get; } = $"{Version.V1}/dex/search";
                public static string LiquidityChange { get; } = $"{Version.V1}/dex/liquidity-change/list";
            }

            public static class Pairs
            {
                public static string SpotPairsLatest { get; } = $"{Version.V4}/dex/spot-pairs/latest";
                public static string QuotesLatest { get; } = $"{Version.V4}/dex/pairs/quotes/latest";
            }

            public static class Platform
            {
                public static string List { get; } = $"{Version.V1}/dex/platform/list";
                public static string Detail { get; } = $"{Version.V1}/dex/platform/detail";
            }

            public static class Kline
            {
                public static string Points { get; } = $"{Version.V1}/k-line/points";
                public static string Candles { get; } = $"{Version.V1}/k-line/candles";
            }

            public static class Holders
            {
                public static string List { get; } = $"{Version.V1}/dex/holders/list";
                public static string Detail { get; } = $"{Version.V1}/dex/holders/detail";
                public static string TrendList { get; } = $"{Version.V1}/dex/holders/trend/list";
                public static string TagCount { get; } = $"{Version.V1}/dex/holders/tag_count";
                public static string Count { get; } = $"{Version.V1}/dex/holders/count";
            }
        }
        public static class Fiat
        {
            public static string Map { get; } = $"{Version.V1}/fiat/map";
        }

        public static class Exchange
        {
            public static string Assets { get; } = $"{Version.V1}/exchange/assets";
            public static string Map { get; } = $"{Version.V1}/exchange/map";
            public static string Info { get; } = $"{Version.V1}/exchange/info";
            public static class Listings
            {
                public static string Latest { get; } = $"{Version.V1}/exchange/listings/latest";
                public static string Historical { get; } = $"{Version.V1}/exchange/listings/historical";
            }
            public static class MarketPairs
            {
                public static string Latest { get; } = $"{Version.V1}/exchange/market-pairs/latest";
            }
            public static class Quotes
            {
                public static string Latest { get; } = $"{Version.V1}/exchange/quotes/latest";
                public static string Historical { get; } = $"{Version.V1}/exchange/quotes/historical";
            }

            // Add more endpoints for the Exchange category here
        }
        public static class GlobalMetrics
        {
            public static class Quotes
            {
                public static string Historical { get; } = $"{Version.V1}/global-metrics/quotes/historical";
                public static string Latest { get; } = $"{Version.V1}/global-metrics/quotes/latest";
            }




        }
        public static class FearAndGreed
        {
            public static string Latest { get; } = $"{Version.V3}/fear-and-greed/latest";
            public static string Historical { get; } = $"{Version.V3}/fear-and-greed/historical";
        }
        public static class Index
        {
            public static string Cmc100Latest { get; } = $"{Version.V3}/index/cmc100-latest";
            public static string Cmc100Historical { get; } = $"{Version.V3}/index/cmc100-historical";
            public static string Cmc20Latest { get; } = $"{Version.V3}/index/cmc20-latest";
            public static string Cmc20Historical { get; } = $"{Version.V3}/index/cmc20-historical";
        }
        public static class Tools
        {
            public static string PriceConversion { get; } = $"{Version.V2}/tools/price-conversion";
        }
        public static class Blockchain
        {
            public static class Statistics
            {
                public static string Latest { get; } = $"{Version.V1}/blockchain/statistics/latest";

            }

        }
        public static class Key
        {
            public static string Info { get; } = $"{Version.V1}/key/info";
        }
        public static class Content
        {
            public static string Latest { get; } = $"{Version.V1}/content/latest";
            public static class Posts
            {
                public static string Comments { get; } = $"{Version.V1}/content/posts/comments";
                public static string Latest { get; } = $"{Version.V1}/content/posts/latest";
                public static string Top { get; } = $"{Version.V1}/content/posts/top";
            }
        }
        public static class Community
        {
            public static class Trending
            {
                public static string Token { get; } = $"{Version.V1}/community/trending/token";
                public static string Topic { get; } = $"{Version.V1}/community/trending/topic";
            }
        }
        // Add more categories and endpoints as needed
    }
}
