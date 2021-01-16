﻿using SearchAggregator.DataAccess.Models;

namespace SearchAggregator.Repositories
{
    public class ResourceRepository : Repository<Resource>, IResourceRepository
    {
        public ResourceRepository(SearchAggregatorContext context) : base(context)
        {
        }
    }
}
