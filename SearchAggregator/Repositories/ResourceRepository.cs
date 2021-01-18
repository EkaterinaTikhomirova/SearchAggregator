using Microsoft.EntityFrameworkCore;
using SearchAggregator.DataAccess.Models;
using System.Threading.Tasks;

namespace SearchAggregator.Repositories
{
    public class ResourceRepository : Repository<Resource>, IResourceRepository
    {
        public ResourceRepository(SearchAggregatorContext context) : base(context)
        {
        }
        public async Task<Resource> UrlExists(string url)
        {
            return await _entity.SingleOrDefaultAsync(p => p.UrlAddress == url);
        }
    }
}
