using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using SearchAggregator.DataAccess.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchAggregator.Services.CustomSearchers
{
    public class GoogleCustomSearcher : SearchService
    {
        public string SubscriptionKey { get; set; }

        public string ConfigID { get; set; }

        public GoogleCustomSearcher(string subscriptionKey, string configID)
        {
            SubscriptionKey = subscriptionKey;
            ConfigID = configID;
        }        

        public override async Task<List<ResourceDTO>> Search(string searchText)
        {
            var customSearch = new CustomsearchService(new BaseClientService.Initializer
            {
                ApiKey = SubscriptionKey
            });

            var result = customSearch.Cse.List(searchText);
            result.Cx = ConfigID;

            var resExecute = await result.ExecuteAsync();
            List<ResourceDTO> dataModel = new List<ResourceDTO>();
            while (dataModel != null && dataModel.Count < 10)
            {
                resExecute.Items?.ToList()
                    .ForEach(x => dataModel.Add(new ResourceDTO
                    {
                        Description = x.Snippet,
                        Title = x.Title,
                        UrlAddress = x.Link
                    }
                ));
            }
            return dataModel;
        }
    }
}
