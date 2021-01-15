using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SearchAggregator.DataAccess.EntityBuilders
{
    public class KeywordResourceBuilder
    {
        public KeywordResourceBuilder(EntityTypeBuilder<KeywordResource> entityBuilder)
        {
            entityBuilder.HasKey(kr => new { kr.KeywordId, kr.ResourceId });

            entityBuilder.HasOne(k => k.Keyword)
                .WithMany(kr => kr.KeywordResources)
                .HasForeignKey(kr => kr.KeywordId);

            entityBuilder.HasOne(r => r.Resource)
                .WithMany(kr => kr.KeywordResources)
                .HasForeignKey(kr => kr.ResourceId);
        }
    }
}
