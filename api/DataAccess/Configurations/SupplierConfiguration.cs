using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Address).IsRequired();
            builder.Property(x => x.City).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.Mail).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.Address).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();
            builder.HasIndex(x => x.Mail).IsUnique();

            builder.HasMany(x => x.Items).WithOne(y => y.Supplier)
                .HasForeignKey(x => x.SupplierId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
