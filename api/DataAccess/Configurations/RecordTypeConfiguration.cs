using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class RecordTypeConfiguration : IEntityTypeConfiguration<RecordType>
    {
        public void Configure(EntityTypeBuilder<RecordType> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.PayChange).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.UserRecords).WithOne(y => y.RecordType)
                .HasForeignKey(x => x.RecordTypeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
