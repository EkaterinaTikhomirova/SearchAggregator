using Microsoft.EntityFrameworkCore;
using SearchAggregator.DataAccess.Models;
using SearchAggregator.DataAccess.EntityBuilders;

namespace SearchAggregator.Repositories
{
    public class SearchAggregatorContext : DbContext
    {
        public SearchAggregatorContext(DbContextOptions<SearchAggregatorContext> options) : base(options)
        { }

        protected SearchAggregatorContext()
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ResourceBuilder(modelBuilder.Entity<Resource>());
            new KeywordBuilder(modelBuilder.Entity<Keyword>());
            new KeywordResourceBuilder(modelBuilder.Entity<KeywordResource>());
        }
    }
}
