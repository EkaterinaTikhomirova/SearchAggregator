using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SearchAggregator.DataAccess.EntityBuilders
{
    public class KeywordBuilder
    {
        public KeywordBuilder(EntityTypeBuilder<Keyword> entityBuilder)
        {
            entityBuilder.HasKey(k => k.Id);
            entityBuilder.Property(k => k.Word).IsRequired();
        }
    }
}
