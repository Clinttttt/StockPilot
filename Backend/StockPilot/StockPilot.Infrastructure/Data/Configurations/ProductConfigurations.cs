using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Infrastructure.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Description).HasMaxLength(200);

            builder.Property(t => t.SellingPrice)
                .HasPrecision(18,2);

            builder.Property(t => t.CostPrice)
                .HasPrecision(18, 2);

            builder.Property(t => t.Sku)
                .HasMaxLength(50);

            builder.HasOne(s => s.Category)
                .WithMany(c => c.Products)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
