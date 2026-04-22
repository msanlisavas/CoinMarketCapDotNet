using System;
using CoinMarketCapDotNet.Api;
using CoinMarketCapDotNet.Configuration;

namespace CoinMarketCapDotNet_Tests.Collection
{
    public class CoinMarketCapAPIFixture : IDisposable
    {
        public CoinMarketCapAPI CoinMarketCapAPI { get; private set; }
        private readonly string _apiKey =
            Environment.GetEnvironmentVariable("CMC_API_KEY") ?? "your-valid-api-key";

        public CoinMarketCapAPIFixture()
        {
            CoinMarketCapAPI = new CoinMarketCapAPI(new CoinMarketCapOptions { ApiKey = _apiKey });
        }

        public void SetSandboxMode(bool useSandbox)
        {
            CoinMarketCapAPI = new CoinMarketCapAPI(new CoinMarketCapOptions
            {
                ApiKey = _apiKey,
                UseSandbox = useSandbox
            });
        }

        public void Dispose()
        {
            CoinMarketCapAPI = new CoinMarketCapAPI(new CoinMarketCapOptions { ApiKey = _apiKey });
        }
    }
}
