using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sauron.Infrastructure.Http
{
    public class HttpRequest
    {
        private const string UserAgent = "Mozilla/5.0 (X11; Linux x86_64) "
                                         + "AppleWebKit/537.36 (KHTML, like Gecko) "
                                         + "Chrome/81.0.4044.141 Safari/537.36";

        private readonly HttpClient _httpClient;

        public HttpRequest()
        {
            _httpClient = new HttpClient(new HttpClientHandler
            {
                CookieContainer = new CookieContainer()
            });
        }

        public Task<string> Download(string url)
        {
            return _httpClient
                   .SendAsync(new HttpRequestMessage
                   {
                       RequestUri = new Uri(url),
                       Method = HttpMethod.Get,
                       Headers =
                       {
                           { "X-Version", "1" },
                           { HttpRequestHeader.UserAgent.ToString(), UserAgent }
                       }
                   })
                   .ContinueWith(antecedent =>
                   {
                       antecedent.Result.EnsureSuccessStatusCode();
                       return antecedent.Result.Content.ReadAsStringAsync();
                   })
                   .Unwrap();
        }
    }
}
