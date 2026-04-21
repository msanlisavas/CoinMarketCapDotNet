using System.Net;

namespace CoinMarketCapDotNet.Models.Exceptions
{
    public sealed class CoinMarketCapRateLimitException : CoinMarketCapException
    {
        public CoinMarketCapRateLimitException(HttpStatusCode statusCode, int? errorCode, string? cmcErrorMessage, string message)
            : base(statusCode, errorCode, cmcErrorMessage, message) { }
    }
}
