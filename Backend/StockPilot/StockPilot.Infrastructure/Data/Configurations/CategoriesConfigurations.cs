using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Infrastructure.Data.Configurations
{
    public class CategoriesConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.Property(s => s.Name).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Description).IsRequired().HasMaxLength(100);
            builder.Property(s => s.IsActive).HasDefaultValue(true);

            builder.HasIndex(c => c.Name).IsUnique();


            builder.HasMany(s => s.Products)
               .WithOne(s => s.Category)
               .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
