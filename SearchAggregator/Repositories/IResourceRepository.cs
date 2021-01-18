using SearchAggregator.DataAccess.Models;
using System.Threading.Tasks;

namespace SearchAggregator.Repositories
{
    public interface IResourceRepository : IRepository<Resource>
    {
        Task<Resource> UrlExists(string url);
    }
}
