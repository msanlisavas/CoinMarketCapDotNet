namespace CoinMarketCapDotNet.Api.General
{
    public static class Endpoints
    {
        public static class Version
        {
            public static string V1 { get; } = "v1";
            public static string V2 { get; } = "v2";
            public static string V3 { get; } = "v3";
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
            }
            // Add more endpoints for the Cryptocurrency category here
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
