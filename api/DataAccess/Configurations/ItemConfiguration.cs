using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.MinQuantity).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.CategoryItems).WithOne(y => y.Item)
                .HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.OrderLines).WithOne(y => y.Item)
                .HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
