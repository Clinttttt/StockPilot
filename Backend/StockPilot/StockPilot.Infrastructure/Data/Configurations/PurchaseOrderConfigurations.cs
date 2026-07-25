using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Infrastructure.Data.Configurations
{
    public class PurchaseOrderConfigurations : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.HasMany(s => s.Items)
                .WithOne(s => s.PurchaseOrder)
                .HasForeignKey(s => s.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(s => s.PoNumber)
                .HasMaxLength(50);
        }
    }
}
