using SearchAggregator.DataAccess.Models;

namespace SearchAggregator.Repositories
{
    public class KeywordRepository : Repository<Keyword>, IKeywordRepository
    {
        public KeywordRepository(SearchAggregatorContext context) : base(context)
        {
        }
    }
}
