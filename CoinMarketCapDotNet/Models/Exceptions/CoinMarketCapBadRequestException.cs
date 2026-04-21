using System.Net;

namespace CoinMarketCapDotNet.Models.Exceptions
{
    public sealed class CoinMarketCapBadRequestException : CoinMarketCapException
    {
        public CoinMarketCapBadRequestException(HttpStatusCode statusCode, int? errorCode, string? cmcErrorMessage, string message)
            : base(statusCode, errorCode, cmcErrorMessage, message) { }
    }
}
