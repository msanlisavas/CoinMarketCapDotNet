using System;

namespace CoinMarketCapDotNet.Configuration
{
    public sealed class CoinMarketCapOptions
    {
        public string ApiKey { get; init; } = string.Empty;

        public bool UseSandbox { get; init; }

        public Uri? BaseAddress { get; init; }

        public TimeSpan? Timeout { get; init; }
    }
}
