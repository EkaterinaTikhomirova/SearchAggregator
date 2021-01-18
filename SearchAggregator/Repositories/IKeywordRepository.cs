using SearchAggregator.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchAggregator.Repositories
{
    public interface IKeywordRepository : IRepository<Keyword>
    {
        Task<IEnumerable<Resource>> GetResourcesAsync(string word);
        void AddResourceToKeyword(KeywordResource keywordResource);
    }
}
