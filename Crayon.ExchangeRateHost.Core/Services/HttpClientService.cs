using System.Net.Http;

namespace Crayon.ExchangeRateHost.Core.Services
{
    /// <summary>
    /// Abstraction
    /// </summary>
    public interface IHttpClientService
    {
        HttpClient CreateHttpClient();
    }

    public class HttpClientService : IHttpClientService
    {
        public HttpClient CreateHttpClient()
        {
            return new HttpClient();
        }
    }
}
