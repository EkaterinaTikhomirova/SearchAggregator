using Google.Apis.Customsearch.v1.Data;
using System.Threading.Tasks;

namespace SearchAggregator.Services.APIClient
{
    public interface IGoogleCustomsearchServiceHandler
    {
        void Initialize(string apiKey);
        void GetCseResource(string searchText);
        void SetConfig(string configId);
        Task<Search> ExecuteAsync();
    }
}
