﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchAggregator.DataAccess.Models;

namespace SearchAggregator.DataAccess.EntityBuilders
{
    public class KeywordBuilder
    {
        public KeywordBuilder(EntityTypeBuilder<Keyword> entityBuilder)
        {
            entityBuilder.HasKey(k => k.Id);
            entityBuilder.Property(k => k.Word).IsRequired();
            entityBuilder.HasIndex(k => k.Word).IsUnique();
        }
    }
}
