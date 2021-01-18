using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchAggregator.Services.APIClient
{
    public class HttpClientHandler : IHttpClientHandler
    {
        private readonly HttpClient _client;
        public HttpClientHandler(string baseUri)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseUri);
        }

        public HttpClientHandler()
        {
            _client = new HttpClient();
        }

        public void AddHeaders(string name, string value)
        {
            _client.DefaultRequestHeaders.Add(name, value);
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _client.GetAsync(url);
        }
    }
}
