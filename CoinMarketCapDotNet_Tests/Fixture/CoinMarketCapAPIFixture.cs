using CoinMarketCapDotNet.Api;
using System;

namespace CoinMarketCapDotNet_Tests.Collection
{
    public class CoinMarketCapAPIFixture : IDisposable
    {
        public CoinMarketCapAPI CoinMarketCapAPI { get; private set; }
        private readonly string _apiKey = "e2bd3ea8-329a-4575-8ca2-6cb67d919618";

        public CoinMarketCapAPIFixture()
        {
            this.CoinMarketCapAPI = new CoinMarketCapAPI(_apiKey);
        }

        public void SetSandboxMode(bool useSandbox)
        {
            this.CoinMarketCapAPI = new CoinMarketCapAPI(_apiKey, useSandbox);
        }

        public void Dispose()
        {
            this.CoinMarketCapAPI = new CoinMarketCapAPI(_apiKey);
        }
    }




}
