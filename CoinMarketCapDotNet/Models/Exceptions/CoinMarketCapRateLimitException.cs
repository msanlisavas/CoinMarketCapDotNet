using System.Net;

namespace CoinMarketCapDotNet.Models.Exceptions
{
    public sealed class CoinMarketCapRateLimitException : CoinMarketCapException
    {
        public CoinMarketCapRateLimitException(int? errorCode, string? cmcErrorMessage, string message)
            : base((HttpStatusCode)429, errorCode, cmcErrorMessage, message) { }
    }
}
