using System;
using System.Net;

namespace DotNetLiberty.Http
{
    public class WebApiClientException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }

        public WebApiClientException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            HttpStatusCode = statusCode;
        }
    }
}