using SearchAggregator.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchAggregator.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<bool> DeleteAsync(Guid id);
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T obj);
        T Update(T obj);
    }
}
