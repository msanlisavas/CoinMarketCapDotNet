using System;
using System.Net;

namespace CoinMarketCapDotNet.Models.Exceptions
{
    public class CoinMarketCapException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public int? ErrorCode { get; }
        public string? CmcErrorMessage { get; }

        public CoinMarketCapException(HttpStatusCode statusCode, int? errorCode, string? cmcErrorMessage, string message)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            CmcErrorMessage = cmcErrorMessage;
        }

        public CoinMarketCapException(HttpStatusCode statusCode, int? errorCode, string? cmcErrorMessage, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            CmcErrorMessage = cmcErrorMessage;
        }
    }
}
