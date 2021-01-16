using SearchAggregator.DataAccess.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchAggregator.Services
{
    public abstract class SearchService
    {
        public virtual void AddSearcher(SearchService seacher) { }
        public virtual void RemoveSeacher(SearchService seacher) { }

        public abstract Task<List<ResourceDTO>> Search(string searchText);
    }
}
