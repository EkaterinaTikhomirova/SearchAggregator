using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SearchAggregator.DataAccess.DTOs;
using SearchAggregator.Services;
using SearchAggregator.Services.APIClient;
using SearchAggregator.Services.CustomSearchers;

namespace SearchAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly SearchService _customSearchService;
        private readonly IKeywordService _keywordService;
        private readonly IConfiguration _configuration;

        public SearchController(SearchService customSearchService, IKeywordService keywordService, IConfiguration configuration)
        {
            _customSearchService = customSearchService;
            _keywordService = keywordService;
            _configuration = configuration;

            customSearchService.AddSearcher(new YandexCustomSeacher(
                _configuration["YandexSearch:SubscriptionKey"],
                _configuration["YandexSearch:UserName"],
                new HttpClientHandler()
                ));
            customSearchService.AddSearcher(new GoogleCustomSearcher(
                _configuration["GoogleSearch:SubscriptionKey"],
                _configuration["GoogleSearch:ConfigID"],
                new GoogleCustomsearchServiceHandler()
                ));
            customSearchService.AddSearcher(new BingCustomSeacher(
                _configuration["BingSearch:SubscriptionKey"],
                _configuration["BingSearch:ConfigID"],
                new HttpClientHandler()
                ));
        }

        [HttpGet]
        public async Task<List<ResourceDTO>> GetSearchResultsAsync(string searchText)
        {
            var resources = await _keywordService.GetResourcesByKeywordAsync(searchText);
            if (resources.Count == 0)
            {
                resources = await _customSearchService.Search(searchText);
                await _keywordService.CreateKeywordAsync(new KeywordDTO
                {
                    Word = searchText,
                    Resources = resources
                });
            }
            return resources;
        }
    }
}
