using Xunit;

namespace CoinMarketCapDotNet_Tests.Collection
{
    [CollectionDefinition("CoinMarketCapAPICollection")]
    public class CoinMarketCapAPICollection : ICollectionFixture<CoinMarketCapAPIFixture>
    {
        // No additional code required
    }

}
