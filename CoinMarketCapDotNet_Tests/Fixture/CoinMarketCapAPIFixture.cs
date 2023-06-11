using CoinMarketCapDotNet.Api;
using System;

namespace CoinMarketCapDotNet_Tests.Collection
{
    public class CoinMarketCapAPIFixture : IDisposable
    {
        public CoinMarketCapAPI CoinMarketCapAPI { get; private set; }
        private readonly string _apiKey = "legit_api_key2";

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
