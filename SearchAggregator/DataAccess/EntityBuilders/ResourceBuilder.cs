﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchAggregator.DataAccess.Models;

namespace SearchAggregator.DataAccess.EntityBuilders
{
    public class ResourceBuilder
    {
        public ResourceBuilder(EntityTypeBuilder<Resource> entityBuilder)
        {
            entityBuilder.HasKey(r => r.Id);
            entityBuilder.Property(r => r.Title).IsRequired();
            entityBuilder.Property(r => r.Description).IsRequired();
        }
    }
}
