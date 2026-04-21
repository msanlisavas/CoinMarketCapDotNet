using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoinMarketCapDotNet.Api
{
    /// <summary>
    /// Abstraction over <see cref="CoinMarketCapAPI"/> for DI and test-double scenarios. Consumers that want to inject the wrapper as an interface rather than the concrete type should depend on this. For most scenarios, injecting the concrete <see cref="CoinMarketCapAPI"/> (with an <see cref="System.Net.Http.HttpClient"/> carrying a stub or mock handler for testing) is sufficient and this interface is not required.
    /// </summary>
    public interface ICoinMarketCapAPI : IDisposable
    {
        /// <summary>Endpoints related to cryptocurrencies.</summary>
        CoinMarketCapAPI.CryptocurrencyEndpoint Cryptocurrency { get; }

        /// <summary>Endpoints related to fiat currencies.</summary>
        CoinMarketCapAPI.FiatEndpoint Fiat { get; }

        /// <summary>Endpoints related to exchanges.</summary>
        CoinMarketCapAPI.ExchangeEndpoint Exchange { get; }

        /// <summary>Endpoints related to global market metrics.</summary>
        CoinMarketCapAPI.GlobalMetricsEndpoint GlobalMetrics { get; }

        /// <summary>Miscellaneous tools and utilities.</summary>
        CoinMarketCapAPI.ToolsEndpoint Tools { get; }

        /// <summary>Endpoints related to blockchain statistics.</summary>
        CoinMarketCapAPI.BlockchainEndpoint Blockchain { get; }

        /// <summary>Endpoints related to API keys.</summary>
        CoinMarketCapAPI.KeyEndpoint Key { get; }

        /// <summary>Endpoints related to content (news, articles, etc.).</summary>
        CoinMarketCapAPI.ContentEndpoint Content { get; }

        /// <summary>Endpoints related to the community.</summary>
        CoinMarketCapAPI.CommunityEndpoint Community { get; }

        /// <summary>CoinMarketCap Fear &amp; Greed Index (v2.1.0+).</summary>
        CoinMarketCapAPI.FearAndGreedEndpoint FearAndGreed { get; }

        /// <summary>CMC 100 and CMC 20 market indices (v2.1.0+).</summary>
        CoinMarketCapAPI.IndexEndpoint Index { get; }

        /// <summary>DEX data — tokens, pairs, platforms, holders, K-line (v2.2.0+).</summary>
        CoinMarketCapAPI.DexEndpoint Dex { get; }

        /// <summary>Low-level GET transport. Most consumers should use the endpoint-group methods instead; this is exposed for calling endpoints the wrapper does not yet cover.</summary>
        /// <typeparam name="T">Expected response body type.</typeparam>
        /// <param name="endpoint">Endpoint path (relative to the API base address), may include a query string.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<T> GetDataAsync<T>(string endpoint, CancellationToken cancellationToken = default) where T : class;

        /// <summary>Low-level POST transport. The body is JSON-serialized with the wrapper's shared <c>JsonSerializerOptions</c> (which omits <c>null</c> fields).</summary>
        /// <typeparam name="T">Expected response body type.</typeparam>
        /// <param name="endpoint">Endpoint path (relative to the API base address).</param>
        /// <param name="body">The request body. Typically an anonymous object of filter parameters.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<T> PostDataAsync<T>(string endpoint, object body, CancellationToken cancellationToken = default) where T : class;
    }
}
