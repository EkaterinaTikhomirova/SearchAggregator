using SearchAggregator.DataAccess.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchAggregator.Services
{
    public class CustomSearchServices : SearchService
    {
        private List<SearchService> _seachers;

        public CustomSearchServices()
        {
            _seachers = new List<SearchService>();
        }

        public override void AddSearcher(SearchService seacher)
        {
            _seachers.Add(seacher);
        }

        public override void RemoveSeacher(SearchService seacher)
        {
            _seachers.Remove(seacher);
        }

        public override async Task<List<ResourceDTO>> Search(string searchText)
        {
            var taskArr = new Task<List<ResourceDTO>>[_seachers.Count];
            for(int i = 0; i < _seachers.Count; i ++)
            {
                taskArr[i] = _seachers[i].Search(searchText);
            }
            var firstTask =  await TaskExtension.WhenFirst(taskArr, t => t.Status == TaskStatus.RanToCompletion);
            return firstTask?.Result;
        }
    }
}
