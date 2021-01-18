using Microsoft.EntityFrameworkCore;
using SearchAggregator.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchAggregator.Repositories
{
    public class KeywordRepository : Repository<Keyword>, IKeywordRepository
    {
        private readonly DbSet<KeywordResource> _keywordResource;
        public KeywordRepository(SearchAggregatorContext context) : base(context)
        {
            _keywordResource = context.Set<KeywordResource>();
        }

        public async Task<IEnumerable<Resource>> GetResourcesAsync(string word)
        {
            return await _entity.Where(r => r.Word == word)
                .SelectMany(r => r.KeywordResources)
                .Select(r => r.Resource)
                .ToListAsync();
        }

        public void AddResourceToKeyword(KeywordResource keywordResource)
        {
            _keywordResource.Add(keywordResource);
        }
    }
}
