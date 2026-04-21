using System.Net;

namespace CoinMarketCapDotNet.Models.Exceptions
{
    public sealed class CoinMarketCapServerException : CoinMarketCapException
    {
        public CoinMarketCapServerException(HttpStatusCode statusCode, int? errorCode, string? cmcErrorMessage, string message)
            : base(statusCode, errorCode, cmcErrorMessage, message) { }
    }
}
