using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class WorkTypeConfiguration : IEntityTypeConfiguration<WorkType>
    {
        public void Configure(EntityTypeBuilder<WorkType> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.HourlyRate).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.Schedules).WithOne(y => y.WorkType)
                .HasForeignKey(x => x.WorkTypeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
