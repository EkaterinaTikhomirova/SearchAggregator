using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using System.Threading.Tasks;
using static Google.Apis.Customsearch.v1.CseResource;

namespace SearchAggregator.Services.APIClient
{
    public class GoogleCustomsearchServiceHandler : IGoogleCustomsearchServiceHandler
    {
        private CustomsearchService _customsearch;

        private ListRequest _result;

        public void Initialize(string apiKey)
        {
            _customsearch = new CustomsearchService(new BaseClientService.Initializer
            {
                ApiKey = apiKey
            });
        }

        public void GetCseResource(string searchText)
        {
            _result = _customsearch.Cse.List(searchText);
        }

        public void SetConfig(string configId)
        {
            _result.Cx = configId;
        }

        public async Task<Search> ExecuteAsync()
        {
            return await _result.ExecuteAsync();
        }
    }
}
