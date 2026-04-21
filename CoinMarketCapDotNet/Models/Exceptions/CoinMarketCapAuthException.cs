using System.Net;

namespace CoinMarketCapDotNet.Models.Exceptions
{
    public sealed class CoinMarketCapAuthException : CoinMarketCapException
    {
        public CoinMarketCapAuthException(HttpStatusCode statusCode, int? errorCode, string? cmcErrorMessage, string message)
            : base(statusCode, errorCode, cmcErrorMessage, message) { }
    }
}
