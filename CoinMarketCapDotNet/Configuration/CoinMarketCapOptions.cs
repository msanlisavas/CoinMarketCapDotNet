using System;

namespace CoinMarketCapDotNet.Configuration
{
    /// <summary>
    /// Configuration for <see cref="CoinMarketCapDotNet.Api.CoinMarketCapAPI"/>. All properties are init-only; construct with a C# object initializer.
    /// </summary>
    public sealed class CoinMarketCapOptions
    {
        /// <summary>
        /// Your CoinMarketCap Pro API key. Required. The API key is sent as the <c>X-CMC_PRO_API_KEY</c> header on every request.
        /// </summary>
        public string ApiKey { get; init; } = string.Empty;

        /// <summary>
        /// When <c>true</c>, requests are sent to <c>sandbox-api.coinmarketcap.com</c> instead of <c>pro-api.coinmarketcap.com</c>. Ignored if <see cref="BaseAddress"/> is set.
        /// </summary>
        public bool UseSandbox { get; init; }

        /// <summary>
        /// Optional base address override. When set, this value is used verbatim (with a trailing slash enforced) and <see cref="UseSandbox"/> is ignored. Useful for proxying or for pointing the wrapper at a test server.
        /// </summary>
        public Uri? BaseAddress { get; init; }

        /// <summary>
        /// Optional HTTP request timeout. When set and no <see cref="System.Net.Http.HttpClient"/> is injected into the API constructor, the wrapper creates a per-instance <see cref="System.Net.Http.HttpClient"/> with this timeout (disposed when the API instance is disposed). When an <see cref="System.Net.Http.HttpClient"/> is injected, this value is ignored — configure the timeout on the injected client instead.
        /// </summary>
        public TimeSpan? Timeout { get; init; }
    }
}
