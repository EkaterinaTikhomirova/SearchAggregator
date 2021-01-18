using System.Net.Http;
using System.Threading.Tasks;

namespace SearchAggregator.Services.APIClient
{
    public interface IHttpClientHandler
    {
        void AddHeaders(string name, string value);
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
