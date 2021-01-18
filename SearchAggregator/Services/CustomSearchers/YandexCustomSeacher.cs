using SearchAggregator.DataAccess.DTOs;
using SearchAggregator.Services.APIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SearchAggregator.Services.CustomSearchers
{
    public class YandexCustomSeacher : SearchService
    {
        public string SubscriptionKey { get; set; }
        public string UserName { get; set; }

        private readonly IHttpClientHandler _client;
        public YandexCustomSeacher(string subscriptionKey, string userName, IHttpClientHandler client)
        {
            SubscriptionKey = subscriptionKey;
            UserName = userName;
            _client = client;
        }

        public override async Task<List<ResourceDTO>> Search(string searchText)
        {
            string urlSearch = "https://yandex.com/search/xml" +
                $"?user={UserName}" +
                $"&key={SubscriptionKey}" +
                $"&query={searchText}" +
                "&l10n=en&sortby=tm.order%3Dascending&filter=none&groupby=attr%3Dd.mode%3Ddeep.groups-on-page%3D10.docs-in-group%3D3";

            var httpResponseMessage = await _client.GetAsync(urlSearch);

            if(!httpResponseMessage.IsSuccessStatusCode)
                throw new Exception($"{nameof(YandexCustomSeacher)} doesn't work");

            var responseContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
            XDocument xmlResponse = XDocument.Parse(responseContent);
            List<ResourceDTO> dataModel = new List<ResourceDTO>();

            XElement results = xmlResponse.Descendants("grouping").SingleOrDefault();

            if (results == null)
                throw new Exception($"{nameof(YandexCustomSeacher)} doesn't find anything");

            foreach (XElement phoneElement in results.Elements("group"))
            {
                XElement url = phoneElement.Element("doc").Element("url");
                XElement title = phoneElement.Element("doc").Element("title");
                XElement description = phoneElement.Element("doc").Element("headline");

                dataModel.Add(new ResourceDTO()
                {
                    UrlAddress = url?.Value,
                    Title = title?.Value,
                    Description = description?.Value
                });
            }
            return dataModel;
        }
    }
}
