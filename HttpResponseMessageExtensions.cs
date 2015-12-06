using System.Net.Http;

namespace DotNetLiberty.Http
{
    public static class HttpResponseMessageExtensions
    {
        public static void ThrowIfUnsuccessful(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new WebApiClientException(response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}