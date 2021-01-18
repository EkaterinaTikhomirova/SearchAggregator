using SearchAggregator.DataAccess.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchAggregator.Services
{
    public interface IKeywordService
    {
        Task CreateKeywordAsync(KeywordDTO keywordDTO);
        Task<List<ResourceDTO>> GetResourcesByKeywordAsync(string word);
    }
}
