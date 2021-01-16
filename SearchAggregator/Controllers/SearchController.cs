using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchAggregator.DataAccess.DTOs;
using SearchAggregator.Services;

namespace SearchAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly SearchService _customSearchService;

        public SearchController(SearchService customSearchService)
        {
            _customSearchService = customSearchService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResourceDTO>>> GetSearchResultsAsync(string name)
        {
            return await _customSearchService.Search(name);
        }
    }
}
