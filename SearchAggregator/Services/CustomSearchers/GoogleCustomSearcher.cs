using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using SearchAggregator.DataAccess.DTOs;
using SearchAggregator.Services.APIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchAggregator.Services.CustomSearchers
{
    public class GoogleCustomSearcher : SearchService
    {
        public string SubscriptionKey { get; set; }
        public string ConfigID { get; set; }

        private readonly IGoogleCustomsearchServiceHandler _googleCustomsearch;

        public GoogleCustomSearcher(string subscriptionKey, string configID, IGoogleCustomsearchServiceHandler googleCustomsearch)
        {
            SubscriptionKey = subscriptionKey;
            ConfigID = configID;
            _googleCustomsearch = googleCustomsearch;
        }        

        public override async Task<List<ResourceDTO>> Search(string searchText)
        {
            _googleCustomsearch.Initialize(SubscriptionKey);
            _googleCustomsearch.GetCseResource(searchText);
            _googleCustomsearch.SetConfig(ConfigID);
            var resExecute = await _googleCustomsearch.ExecuteAsync();

            if(resExecute.Items == null)
                throw new Exception($"{nameof(GoogleCustomSearcher)} doesn't find anything");

            List<ResourceDTO> dataModel = new List<ResourceDTO>();
            resExecute.Items.ToList()
                .ForEach(x => dataModel.Add(new ResourceDTO
                {
                    Description = x.Snippet,
                    Title = x.Title,
                    UrlAddress = x.Link
                }
            ));
            return dataModel;
        }
    }
}
