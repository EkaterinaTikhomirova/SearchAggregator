using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using SearchAggregator.DataAccess.Models;
using System.Threading.Tasks;

namespace SearchAggregator.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _entity;
        public Repository(SearchAggregatorContext context)
        {
            _entity = context.Set<T>();
        }
        public void Add(T obj)
        {
            _entity.Add(obj);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var obj = await _entity.SingleAsync(p => p.Id == id);
            if (obj is null)
            {
                return false;
            }

            _entity.Remove(obj);

            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entity.ToArrayAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _entity.SingleOrDefaultAsync(p => p.Id == id);
        }

        public T Update(T obj)
        {
            _entity.Update(obj);
            return obj;
        }
    }
}
