using Newtonsoft.Json;
using SearchAggregator.DataAccess.DTOs;
using SearchAggregator.Services.APIClient;
using SearchAggregator.Services.CustomSearchers.SeacherResponses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchAggregator.Services.CustomSearchers
{
    public class BingCustomSeacher : SearchService
    {
        public string SubscriptionKey { get; set; }
        public string ConfigID { get; set; }

        private readonly IHttpClientHandler _client;

        public BingCustomSeacher(string subscriptionKey, string configID, IHttpClientHandler client)
        {
            SubscriptionKey = subscriptionKey;
            ConfigID = configID;
            _client = client;
        }

        public override async Task<List<ResourceDTO>> Search(string searchText)
        {
            var url = "https://api.bing.microsoft.com/v7.0/custom/search" +
                $"?q={searchText}" +
                $"&customconfig={ConfigID}" +
                $"&mkt=en-US";

            _client.AddHeaders("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var httpResponseMessage = await _client.GetAsync(url);

            if (!httpResponseMessage.IsSuccessStatusCode)
                throw new Exception($"{nameof(BingCustomSeacher)} doesn't work");

            var responseContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
            BingCustomSearchResponse response = JsonConvert.DeserializeObject<BingCustomSearchResponse>(responseContent);

            if(response.webPages == null)
                throw new Exception($"{nameof(YandexCustomSeacher)} doesn't find anything");

            List<ResourceDTO> resources = new List<ResourceDTO>();
            for (int i = 0; i < response.webPages.value.Length; i++)
            {
                var webPage = response.webPages.value[i];

                resources.Add(new ResourceDTO
                {
                    UrlAddress = webPage.url,
                    Title = webPage.name,
                    Description = webPage.snippet
                });
            }
            return resources;
        }
    }
}
